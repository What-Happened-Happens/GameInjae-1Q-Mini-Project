using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 효과음 관리용 
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

    // 사운드 제어용 bool 
    private bool _isMuted; // 음소거  
    private bool _isSoundPlaying; // 사운드 재생 
    private bool _isSoundStoped;  // 사운드 멈춤 

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
        // BGM 오디오 소스 
        BGMAudioSource = audioObject.GetComponent<AudioSource>();
        BGMAudioSource.volume = BGMSlider.value; 
    }

    // 배경음 셋팅 
    public void SetBgmVolumeFromSlider(float volume)
    {
        if (BGMSlider == null || BGMAudioSource == null) return;

        BGMAudioSource.volume = volume; 

        Debug.Log($"Slider 값에 따라 오디오 볼륨 변경");
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
