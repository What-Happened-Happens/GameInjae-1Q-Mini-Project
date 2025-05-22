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
    private float[] originalVolumes = new float[3];

    public void SetAudioVolume(EAudioMixerType type, float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(type.ToString(), dB);
        originalVolumes[(int)type] = dB;

        // ������ ������ ���Ұ� ������ ��, ������ ������ ����
        if (isMute[(int)EAudioMixerType.Master])
        {
            isMute[(int)EAudioMixerType.Master] = false;
            audioMixer.SetFloat(EAudioMixerType.Master.ToString(), originalVolumes[(int)EAudioMixerType.Master]);
        }

    }

    public void SetAudioMute(EAudioMixerType type)
    {
        int index = (int)type;
        if (!isMute[index])
        {
            isMute[index] = true;
            audioMixer.GetFloat(type.ToString(), out float currentVolume);
            originalVolumes[index] = currentVolume;
            audioMixer.SetFloat(type.ToString(), -80f); // ���� ����
        }
        else
        {
            isMute[index] = false;
            audioMixer.SetFloat(type.ToString(), originalVolumes[index]);
        }
    }

    public float GetVolume(EAudioMixerType type)
    {
        if (audioMixer.GetFloat(type.ToString(), out float dB))
        {
            return Mathf.Pow(10f, dB / 20f); // dB �� 0~1 �� ��ȯ
        }
        return 1f; // �⺻��
    }

    public bool ToggleMute(EAudioMixerType type)
{
    int index = (int)type;

    if (!isMute[index])
    {
        isMute[index] = true;
        audioMixer.GetFloat(type.ToString(), out float currentVolume);
        originalVolumes[index] = currentVolume;
        audioMixer.SetFloat(type.ToString(), -80f);
    }
    else
    {
        isMute[index] = false;
        audioMixer.SetFloat(type.ToString(), originalVolumes[index]);
    }

    return isMute[index]; // true = ���Ұ� ����
}
}
