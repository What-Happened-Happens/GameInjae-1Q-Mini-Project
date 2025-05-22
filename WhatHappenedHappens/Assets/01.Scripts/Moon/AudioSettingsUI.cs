using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public AudioMixerManager mixerManager;

    [Header("UI Elements")]
    public Slider sliderBGM;
    public Slider sliderSFX;
    public Button muteAllButton;

    void Start()
    {
        // 1. �����̴� �ʱ�ȭ
        sliderBGM.value = mixerManager.GetVolume(EAudioMixerType.BGM);
        sliderSFX.value = mixerManager.GetVolume(EAudioMixerType.SFX);

        // 2. ������ ���
        sliderBGM.onValueChanged.AddListener((value) =>
        {
            mixerManager.SetAudioVolume(EAudioMixerType.BGM, value);
        });

        sliderSFX.onValueChanged.AddListener((value) =>
        {
            mixerManager.SetAudioVolume(EAudioMixerType.SFX, value);
        });

        // 3. ���Ұ� ��ư
        muteAllButton.onClick.AddListener(() =>
        {
            bool isMuted = mixerManager.ToggleMute(EAudioMixerType.Master);

            // ������ ������ 0 �Ǵ� ���� ������ ���� (�����̴��� �ݿ�)
            sliderBGM.value = isMuted ? 0f : mixerManager.GetVolume(EAudioMixerType.BGM);
            sliderSFX.value = isMuted ? 0f : mixerManager.GetVolume(EAudioMixerType.SFX);
        });
    }
}
