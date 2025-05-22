using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource sfxSource_player;
    public AudioSource loopSFXSource; // �ȱ� �Ҹ� �� ������
    public AudioSource UISource; // UI �Ҹ�

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


    // ���� �ε�� �� ȣ��Ǵ� �Լ�
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGMForScene(scene.name);  // ���� �� �̸��� ���� ������� ���
    }


    // Ư�� ���� �´� ������� ���
    void PlayBGMForScene(string sceneName)
    {

    }


    // --------------------------------------

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

    // ������ ȿ���� ���
    public void PlayLoopSFX(string name, float pitch = 1f)
    {
        if (loopSFXSource.isPlaying) return; // �̹� ��� ���̸� ����

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
