using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

// ȿ���� ������ 
public enum SoundMixerType // 'public'���� �����Ͽ� ���� ���ɼ� �ϰ��� ���� �ذ�
{
    MASTER,
    BGM,
    FSX,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("AudioValueFromSlider")]
    public AudioMixer audioMixer;
    public Slider BGMaudioSlider;
    public Slider SFXaudioSlider;

    private bool[] _isMuted = new bool[3];
    private bool _isSoundPlaying;
    private bool _isSoundStop;

    private float[] audiVolumes = new float[3];

    private void Awake()
    {
        Instance = this;
    }

    public void SetAudioVolume(SoundMixerType mixerType, float volume)
    {
        audioMixer.SetFloat(mixerType.ToString(), Mathf.Log10(volume) * 20); 
    }

    public void SetAudioMute(SoundMixerType mixerType)
    {
        int type = (int) mixerType; 
        if (!_isMuted[type])
        {
            _isMuted[type] = true;
            audioMixer.GetFloat(mixerType.ToString(), out float curVolume);
            audiVolumes[type] = curVolume;
            SetAudioVolume(mixerType, 0.001f);
        }
        else
        {
            _isMuted[type] = false; 
            SetAudioVolume(mixerType, audiVolumes[type]);
        }
    }

    private void onMute(float volume)
    {
        SoundManager.Instance.SetAudioVolume(SoundMixerType.MASTER, volume); 
    }

}
