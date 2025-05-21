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

    public bool isClipLength { get; set; }    // ����� clip �� ���̸� ��� �� ��, �ƴϸ� ª�� �� �� ����. 
    public bool isStageClear { get;  set; }   // �������� Ŭ��� üũ 

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
  
        _stopCoroutines = new Dictionary<AudioSource, Coroutine>();
    }

    public void PlayStateClip(AudioSource source, SFXState state, bool isShort)
    {
        // ���� ȿ������ �ʿ��� ���� �� ���� ������ ������� ����� ��
        // AudioSource ������Ʈ�� ���� ������Ʈ��, targetOuput �� ã�´�.
        source.volume = 0f; 
        var entry = stateClips.FirstOrDefault(sc => sc.state == state && 
                                              sc.targetOutput == source);
   
        if (entry.clip == null || entry.targetOutput == null)
        {
            Debug.LogWarning($"SFXAudioManager: '{state}' ���¿� �Ҵ�� Ŭ���� �����ϴ�.");
            return;
        }       

        // ª�� ����� ��, ��� ����� clip ���̿� ���缭 ����� �� isShort ���� ���� �����ȴ�. 
        float duration = isShort ? ShortDuration : LongDuration;
        Debug.Log($"��� ���� : {duration}");
        PlayClipWithDuration(source, entry.clip, duration, volumeScale);
    }

    private void PlayClipWithDuration(AudioSource source, AudioClip clip, float duration, float volume)
    {
        if (source == null)
        {
            Debug.LogError("SFXAudioManager: ���޵� AudioSource�� null �Դϴ�.");
            return;
        }

        // ���� �ڷ�ƾ�� ���������� ����
        if (_stopCoroutines.TryGetValue(source, out var prevRoutine))
        {
            Debug.Log($"SFXAudioManager : ������ �÷����� ���尡 ���Ƽ� �÷��� ���Դϴ�. ������Ű�ڽ��ϴ�.");
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
