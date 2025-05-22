using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// 사용 예시

// 배경음악 재생
// SoundManager.Instance.PlayBGM(SoundManager.Instance.bgmClips[0]);

// 캐릭터가 점프할 때
// SoundManager.Instance.PlaySFX("Jump");

// UI 버튼 클릭 시
/*
public void OnButtonClick()
{
    SoundManager.Instance.PlaySFX("UIClick");
}
*/


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource sfxSource_player;
    public AudioSource loopSFXSource; // 걷기 소리 등 루프용
    public AudioSource UISource; // UI 소리

    [Header("Audio Clips")]
    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    private Dictionary<string, AudioClip> sfxDict = new();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitSFXDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitSFXDictionary()
    {
        sfxDict = new Dictionary<string, AudioClip>();
        foreach (var clip in sfxClips)
        {
            sfxDict[clip.name] = clip;
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(string name, float pitch = 1f)
    {
        if (sfxSource == null) return;

        if (sfxDict.TryGetValue(name, out var clip))
        {
            sfxSource.pitch = pitch;
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX not found: " + name);
        }
    }

    public void PlaySFX_player(string name, float pitch = 1f)
    {
        if (sfxSource == null) return;

        if (sfxDict.TryGetValue(name, out var clip))
        {
            sfxSource_player.pitch = pitch;
            sfxSource_player.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX not found: " + name);
        }
    }

    // 루프용 효과음 재생
    public void PlayLoopSFX(string name, float pitch = 1f)
    {
        if (loopSFXSource.isPlaying) return; // 이미 재생 중이면 무시

        if (sfxDict.TryGetValue(name, out var clip))
        {
            loopSFXSource.clip = clip;
            loopSFXSource.pitch = pitch;
            loopSFXSource.loop = true;
            loopSFXSource.Play();
        }
    }

    public void UISFX(string name, float pitch = 1f)
    {
        if (UISource == null) return;

        if (sfxDict.TryGetValue(name, out var clip))
        {
            UISource.pitch = pitch;
            UISource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX not found: " + name);
        }
    }

    public void StopLoopSFX()
    {
        if (loopSFXSource.isPlaying)
        {
            loopSFXSource.Stop();
            loopSFXSource.clip = null;
            loopSFXSource.pitch = 1f;
        }
    }
}
