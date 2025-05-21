using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum SFXState
{
    None = 0,
    Click = 1,
    Object = 2
}

[Serializable]
public struct StateClip
{
    public SFXState state;
    public AudioSource targetOutput;
    public AudioClip clip;
}
public class SFXAudioManager : MonoBehaviour
{

    [Header("Playeback Setting")]
    public float ShortDuration = 0.1f;
    public float LongDuration = 1f;
    public float volumeScale = 0.5f;

    public List<StateClip> stateClips = new List<StateClip>();
    private Dictionary<AudioSource, Coroutine> _stopCoroutines;

    public bool isClipLength { get; set; }    // 재생할 clip 의 길이를 길게 할 지, 아니면 짧게 할 지 결정. 
    public bool isStageClear { get;  set; }   // 스테이지 클리어를 체크 

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
  
        _stopCoroutines = new Dictionary<AudioSource, Coroutine>();
    }

    public void PlayStateClip(AudioSource source, SFXState state, bool isShort)
    {
        // 사운드 효과음이 필요한 상태 에 따라서 실제로 오디오가 출력이 될
        // AudioSource 컴포넌트를 가진 오브젝트인, targetOuput 을 찾는다.
        source.volume = 0f; 
        var entry = stateClips.FirstOrDefault(sc => sc.state == state && 
                                              sc.targetOutput == source);
   
        if (entry.clip == null || entry.targetOutput == null)
        {
            Debug.LogWarning($"SFXAudioManager: '{state}' 상태에 할당된 클립이 없습니다.");
            return;
        }       

        // 짧게 재생할 지, 길게 오디오 clip 길이에 맞춰서 재생할 지 isShort 값에 따라 결정된다. 
        float duration = isShort ? ShortDuration : LongDuration;
        Debug.Log($"재생 길이 : {duration}");
        PlayClipWithDuration(source, entry.clip, duration, volumeScale);
    }

    private void PlayClipWithDuration(AudioSource source, AudioClip clip, float duration, float volume)
    {
        if (source == null)
        {
            Debug.LogError("SFXAudioManager: 전달된 AudioSource가 null 입니다.");
            return;
        }

        // 이전 코루틴이 남아있으면 정지
        if (_stopCoroutines.TryGetValue(source, out var prevRoutine))
        {
            Debug.Log($"SFXAudioManager : 이전에 플레이한 사운드가 남아서 플레이 중입니다. 정지시키겠습니다.");
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
