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
        // 1. 슬라이더 초기화
        sliderBGM.value = mixerManager.GetVolume(EAudioMixerType.BGM);
        sliderSFX.value = mixerManager.GetVolume(EAudioMixerType.SFX);

        // 2. 리스너 등록
        sliderBGM.onValueChanged.AddListener((value) =>
        {
            mixerManager.SetAudioVolume(EAudioMixerType.BGM, value);
        });

        sliderSFX.onValueChanged.AddListener((value) =>
        {
            mixerManager.SetAudioVolume(EAudioMixerType.SFX, value);
        });

        // 3. 음소거 버튼
        muteAllButton.onClick.AddListener(() =>
        {
            bool isMuted = mixerManager.ToggleMute(EAudioMixerType.Master);

            // 볼륨을 강제로 0 또는 이전 값으로 설정 (슬라이더도 반영)
            sliderBGM.value = isMuted ? 0f : mixerManager.GetVolume(EAudioMixerType.BGM);
            sliderSFX.value = isMuted ? 0f : mixerManager.GetVolume(EAudioMixerType.SFX);
        });
    }
}
