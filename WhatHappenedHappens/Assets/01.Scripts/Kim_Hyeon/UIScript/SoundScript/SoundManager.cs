using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    
    private AudioSource BGMAudioSource;

    [Header("AudioObject And AudioClip List")]
    public GameObject audioObject;
    public List<AudioClip> SFXAudioClip = new List<AudioClip>();

    [Header("SoundSliders")]
    public Slider BGMSlider;
    public Slider SFXSlider;

    // ���� ����� bool 
    private bool _isMuted;
    private bool _isSoundPlaying;
    private bool _isSoundStoped;

    private static SoundManager instance; 
    public static SoundManager Instance
    {
        get { 
            if (instance == null) 
                instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;  
            return instance; 
        } 
    }

    private void Start()
    {
        // BGM ����� �ҽ� 
        BGMAudioSource = audioObject.GetComponent<AudioSource>();

    }
    public void SetBgmVolumeFromSlider(AudioSource audiosource, Slider soundSlider)
    {
        if (audiosource == null || soundSlider == null) return;

        audiosource.volume = soundSlider.value;
        Debug.Log($"Slider ���� ���� ����� ���� ����");
    }

}
