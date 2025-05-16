using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ȿ���� ������ 
enum eSound
{
    click, 
    shoot, 
    hit 
}

public class SoundManager : MonoBehaviour
{
    const string SoundPath = "../Sounds"; 
    private AudioSource BGMAudioSource;

    [Header("AudioObject And AudioClip List")]
    public GameObject audioObject;
    public List<AudioClip> SFXAudioClip = new List<AudioClip>();

    [Header("SoundSliders")]
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;

    // ���� ����� bool 
    private bool _isMuted; // ���Ұ�  
    private bool _isSoundPlaying; // ���� ��� 
    private bool _isSoundStoped;  // ���� ���� 

    // SingleTon
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;
            return instance;
        }
    }

    private void Start()
    {
        // BGM ����� �ҽ� 
        BGMAudioSource = audioObject.GetComponent<AudioSource>();
        BGMAudioSource.volume = BGMSlider.value; 
    }

    // ����� ���� 
    public void SetBgmVolumeFromSlider(float volume)
    {
        if (BGMSlider == null || BGMAudioSource == null) return;

        BGMAudioSource.volume = volume; 

        Debug.Log($"Slider ���� ���� ����� ���� ����");
    }

    //public void SetSoundClip(string clipName, Vector3 audioPos, bool loop = false, float volume = 1.0f)
    //{

    //}
    //public void ResetSoundClip()
    //{

    //}

    public static implicit operator SoundManager(AudioPool v)
    {
        throw new NotImplementedException();
    }
}
