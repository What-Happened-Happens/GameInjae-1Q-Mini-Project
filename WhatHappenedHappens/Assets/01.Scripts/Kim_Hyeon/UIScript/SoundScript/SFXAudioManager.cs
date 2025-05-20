using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum FSXState
{
    None = 0,
    Click = 1,
    Object = 2
}

[Serializable]
public struct StateClip
{
    public FSXState state;
    public AudioSource target;
    public AudioClip cllip;
}
public class SFXAudioManager : MonoBehaviour
{

    [Header("Playeback Setting")]
    public float ShortDuration = 0.1f;
    public float LongDuration = 1f;
    public float volumeScale = 0.5f;

    public List<StateClip> stateClips = new List<StateClip>();
    private Dictionary<AudioSource, Coroutine> _stopCoroutines;
    public bool isShort { get; set; }
    public bool isLong { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _stopCoroutines = new Dictionary<AudioSource, Coroutine>();
    }  

    public void PlayStateClip(AudioSource source, FSXState state, bool isShort)
    {
        // 1) 매핑 리스트에서 해당 상태의 AudioClip 찾아내기
        var entry = stateClips.FirstOrDefault(sc => sc.state == state);

        if (entry.cllip == null || entry.target == null)
        {
            Debug.LogWarning($"FSXAudioManager: '{state}' 상태에 할당된 클립이 없습니다.");
            return;
        }
        var targetObj = entry.target.GetComponent<AudioSource>();
        if (targetObj == null)
        {
            Debug.LogError($"{entry.target.name}에 AudioSource가 없습니다.");
            return;
        }
        float duration = isShort ? ShortDuration : LongDuration;
        Debug.Log($"재생 길이 : {duration}");
        PlayClipWithDuration(targetObj, entry.cllip, duration, volumeScale);
    }

    private void PlayClipWithDuration(AudioSource source, AudioClip clip, float duration, float volume)
    {
        if (source == null)
        {
            Debug.LogError("FSXAudioManager: 전달된 AudioSource가 null 입니다.");
            return;
        }

        // 이전 코루틴이 남아있으면 정지
        if (_stopCoroutines.TryGetValue(source, out var prevRoutine))
        {
            StopCoroutine(prevRoutine);
        }

        source.clip = clip;
        source.volume = volume;
        source.Play();

        // 재생 종료 예약
        _stopCoroutines[source] = StartCoroutine(StopAfterDuration(source, duration));
    }

    private IEnumerator StopAfterDuration(AudioSource source, float duration)
    {
        // 클립 길이보다 길게 요청되면 짧게 맞추기
        float wait = Mathf.Min(duration, source.clip.length);
        yield return new WaitForSeconds(wait);

        source.Stop();
        _stopCoroutines.Remove(source);
    }
}
