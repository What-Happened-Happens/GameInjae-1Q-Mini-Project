using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ����

// ������� ���
// SoundManager.Instance.PlayBGM("MainTheme");

// ĳ���Ͱ� ������ ��
// SoundManager.Instance.PlaySFX("Jump");

// UI ��ư Ŭ�� ��
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

    public void PlaySFX(string name)
    {
        if (sfxDict.ContainsKey(name))
        {
            sfxSource.PlayOneShot(sfxDict[name]);
        }
    }
}
