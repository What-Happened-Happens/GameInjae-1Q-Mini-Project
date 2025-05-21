using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// 1. Inspector â���� ����� ���� / ����� ����� �ҽ� ������Ʈ�� �ִ� ������Ʈ / ����� ����� Ŭ���� ���.
// 1-1. SFXAudioPlay�� �� �ִ� ������Ʈ���, SFXAudioManager�� Inspector â���� ��ϵ� ���� / ������Ʈ / Ŭ���� �����ϰ� ����ؾ� ��. 
// 2. AudioSource ������Ʈ�� �ִ� ������Ʈ���� ����� Ŭ��, �ҽ��� ����־ ��� ����. 
// 3. �� ��ũ��Ʈ ���� �ִ� �Լ��� ����� ���� ����� ������� ���� ��ũ��Ʈ�� SFXAudioPlay.cs �� ����. 
// 4. �׷��� ���� ����ϰ� �ʹٸ� PlayStateClip �Լ� ���. 
public enum SFXState
{
    NONE,
    CLICK,
    OBJECT
}

[Serializable]
public struct StateClip
{
    public SFXState state;
    public AudioSource targetOutput;
    public AudioClip clip;
}
public class SFXAudioManager : AudioManager
{
    private static SFXAudioManager instance = null;
    public static SFXAudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(SFXAudioManager)) as SFXAudioManager;
            }
            return instance;
        }
    }

    [Header("Playeback Setting")]
    public float ShortDuration = 0.1f;
    public float LongDuration = 1f;
    public float volumeScale = 0.5f;

    public List<StateClip> stateClips = new List<StateClip>();
    private Dictionary<AudioSource, Coroutine> _stopCoroutines;

    public bool IsClipLength { get; set; }    // ����� clip �� ���̸� ��� �� ��, �ƴϸ� ª�� �� �� ����. : ���(false)�� Ŭ���� ���� ���̷� ��� 
    public bool IsStageClear { get; set; }   // �������� Ŭ��� üũ 
    public bool IsLoop { get; set; } = false; // ���� ���� üũ    
    public SFXState SFXstate { get; set; } = SFXState.NONE; // ���� ������� ���� ���� üũ 

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _stopCoroutines = new Dictionary<AudioSource, Coroutine>();
        volumeScale = 0.5f; 
    }

    // �ܺ� ��ũ��Ʈ���� �� �Լ��� ȣ���ؼ� ����� ȿ������ ���. 
    public void PlayStateClip(AudioSource source, SFXState state, bool isClipLength, bool isLoop)
    {
        if (isMute() == true)
        {
            Debug.Log($"���� ���Ұ� �����Դϴ�. ���� ����� ���� �ʽ��ϴ�.");
            return;
        }
        // ���� ȿ������ �ʿ��� ���� �� ���� ������ ������� ����� ��
        // AudioSource ������Ʈ�� ���� ������Ʈ��, targetOuput �� ã�´�.
        source.volume = 0f;
        var entry = stateClips.FirstOrDefault(sc => sc.state == state &&
                                              sc.targetOutput == source);
        Debug.Log($"PlayStateClip : " +
                  $"��ϵ� ������ AudioSource�� ã���ϴ�. " +
                  $"AudioSource : {entry.targetOutput.ToString()}" +
                  $" State =      {entry.state.ToString()}, " +
                  $" clip =       {entry.clip.ToString()}");

        if (entry.clip == null || entry.targetOutput == null)
        {
            Debug.LogWarning($"SFXAudioManager: '{state}' ���¿� �Ҵ�� Ŭ���� �����ϴ�.");
            return;
        }

        // ª�� ����� ��, ��� ����� clip ���̿� ���缭 ����� �� isShort ���� ���� �����ȴ�. 
        float duration = isClipLength ? ShortDuration : LongDuration;

        Debug.Log($"��� ���� : {duration}");
        PlayClipWithDuration(source, entry.clip, duration, volumeScale, isLoop);
    }

    private void PlayClipWithDuration(AudioSource source, AudioClip clip, float duration, float volume, bool isLoop)
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

        // Loop ���� ����        
        source.loop = isLoop;
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

    public bool isMute()
    {
        if (_isMute)
        {
            Debug.Log($"���� ���Ұ� �����Դϴ�.");
            return _isMute = true;
        }
        else
        {
            Debug.Log($"���� ���Ұ� ���� �����Դϴ�.");
            return _isMute = false;
        }
    }
}
