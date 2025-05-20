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
        // 1) ���� ����Ʈ���� �ش� ������ AudioClip ã�Ƴ���
        var entry = stateClips.FirstOrDefault(sc => sc.state == state);

        if (entry.cllip == null || entry.target == null)
        {
            Debug.LogWarning($"FSXAudioManager: '{state}' ���¿� �Ҵ�� Ŭ���� �����ϴ�.");
            return;
        }
        var targetObj = entry.target.GetComponent<AudioSource>();
        if (targetObj == null)
        {
            Debug.LogError($"{entry.target.name}�� AudioSource�� �����ϴ�.");
            return;
        }
        float duration = isShort ? ShortDuration : LongDuration;
        Debug.Log($"��� ���� : {duration}");
        PlayClipWithDuration(targetObj, entry.cllip, duration, volumeScale);
    }

    private void PlayClipWithDuration(AudioSource source, AudioClip clip, float duration, float volume)
    {
        if (source == null)
        {
            Debug.LogError("FSXAudioManager: ���޵� AudioSource�� null �Դϴ�.");
            return;
        }

        // ���� �ڷ�ƾ�� ���������� ����
        if (_stopCoroutines.TryGetValue(source, out var prevRoutine))
        {
            StopCoroutine(prevRoutine);
        }

        source.clip = clip;
        source.volume = volume;
        source.Play();

        // ��� ���� ����
        _stopCoroutines[source] = StartCoroutine(StopAfterDuration(source, duration));
    }

    private IEnumerator StopAfterDuration(AudioSource source, float duration)
    {
        // Ŭ�� ���̺��� ��� ��û�Ǹ� ª�� ���߱�
        float wait = Mathf.Min(duration, source.clip.length);
        yield return new WaitForSeconds(wait);

        source.Stop();
        _stopCoroutines.Remove(source);
    }
}
