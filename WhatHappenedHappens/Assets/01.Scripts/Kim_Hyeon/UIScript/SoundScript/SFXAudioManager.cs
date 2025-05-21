using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// 1. Inspector 창에서 오디오 상태 / 재생할 오디오 소스 컴포넌트가 있는 오브젝트 / 재생할 오디오 클립을 등록.
// 1-1. SFXAudioPlay가 들어가 있는 오브젝트라면, SFXAudioManager의 Inspector 창에서 등록된 상태 / 오브젝트 / 클립을 동일하게 등록해야 함. 
// 2. AudioSource 컴포넌트가 있는 오브젝트에는 오디오 클립, 소스가 비어있어도 상관 없음. 
// 3. 이 스크립트 내에 있는 함수를 사용해 따로 오디오 재생만을 모은 스크립트가 SFXAudioPlay.cs 에 있음. 
// 4. 그러나 따로 사용하고 싶다면 PlayStateClip 함수 사용. 
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

    public bool IsClipLength { get; set; }    // 재생할 clip 의 길이를 길게 할 지, 아니면 짧게 할 지 결정. : 길게(false)는 클립의 원래 길이로 재생 
    public bool IsStageClear { get; set; }   // 스테이지 클리어를 체크 
    public bool IsLoop { get; set; } = false; // 사운드 루프 체크    
    public SFXState SFXstate { get; set; } = SFXState.NONE; // 현재 재생중인 사운드 상태 체크 

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _stopCoroutines = new Dictionary<AudioSource, Coroutine>();
        volumeScale = 0.5f; 
    }

    // 외부 스크립트에서 이 함수를 호출해서 오디오 효과음을 재생. 
    public void PlayStateClip(AudioSource source, SFXState state, bool isClipLength, bool isLoop)
    {
        if (isMute() == true)
        {
            Debug.Log($"사운드 음소거 상태입니다. 사운드 재생을 하지 않습니다.");
            return;
        }
        // 사운드 효과음이 필요한 상태 에 따라서 실제로 오디오가 출력이 될
        // AudioSource 컴포넌트를 가진 오브젝트인, targetOuput 을 찾는다.
        source.volume = 0f;
        var entry = stateClips.FirstOrDefault(sc => sc.state == state &&
                                              sc.targetOutput == source);
        Debug.Log($"PlayStateClip : " +
                  $"등록된 상태의 AudioSource를 찾습니다. " +
                  $"AudioSource : {entry.targetOutput.ToString()}" +
                  $" State =      {entry.state.ToString()}, " +
                  $" clip =       {entry.clip.ToString()}");

        if (entry.clip == null || entry.targetOutput == null)
        {
            Debug.LogWarning($"SFXAudioManager: '{state}' 상태에 할당된 클립이 없습니다.");
            return;
        }

        // 짧게 재생할 지, 길게 오디오 clip 길이에 맞춰서 재생할 지 isShort 값에 따라 결정된다. 
        float duration = isClipLength ? ShortDuration : LongDuration;

        Debug.Log($"재생 길이 : {duration}");
        PlayClipWithDuration(source, entry.clip, duration, volumeScale, isLoop);
    }

    private void PlayClipWithDuration(AudioSource source, AudioClip clip, float duration, float volume, bool isLoop)
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

        // Loop 여부 결정        
        source.loop = isLoop;
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

    public bool isMute()
    {
        if (_isMute)
        {
            Debug.Log($"사운드 음소거 상태입니다.");
            return _isMute = true;
        }
        else
        {
            Debug.Log($"사운드 음소거 해제 상태입니다.");
            return _isMute = false;
        }
    }
}
