using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum EAudioMixerType { Master, BGM, SFX }

public class AudioMixerManager : MonoBehaviour
{
    [Header("Audio Mixxer")]
    public AudioMixer audioMixer;

    private bool[] isMute = new bool[3];
    private float[] audioVolumes = new float[3];


    public void SetAudioVolume(EAudioMixerType audioMixerType, float volume)
    {
        // ����� �ͼ��� ���� -80 ~ 0�����̱� ������ 0.0001 ~ 1�� Log10 * 20�� �Ѵ�.
        audioMixer.SetFloat(audioMixerType.ToString(), Mathf.Log10(volume) * 20);
    }

    public void SetAudioMute(EAudioMixerType audioMixerType)
    {
        int type = (int)audioMixerType;
        if (!isMute[type]) // ��Ʈ 
        {
            isMute[type] = true;
            audioMixer.GetFloat(audioMixerType.ToString(), out float curVolume);
            audioVolumes[type] = curVolume;
            SetAudioVolume(audioMixerType, 0.001f);
        }
        else
        {
            isMute[type] = false;
            SetAudioVolume(audioMixerType, audioVolumes[type]);
        }
    }
    /*
    private void Mute()
    {
        AudioManager.Instance.SetAudioMute(EAudioMixerType.BGM);
    }

    private void ChangeVolume(float volume)
    {
        AudioManager.Instance.SetAudioVolume(EAudioMixerType.BGM, volume);
    }
    */
}
