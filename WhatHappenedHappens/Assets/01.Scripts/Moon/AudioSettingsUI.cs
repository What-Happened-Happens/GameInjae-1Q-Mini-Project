using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public AudioMixerManager mixerManager;

    public GameObject audioSettingsUI;
    public GameObject pauseScreen;

    [Header("UI Elements")]
    public Slider sliderBGM;
    public Slider sliderSFX;
    public Button muteAllButton;
    public Button BackButton;

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
            SoundManager.Instance.UISFX("UI_Click", 1f);

            bool isMuted = mixerManager.ToggleMute(EAudioMixerType.Master);

            // 볼륨을 강제로 0 또는 이전 값으로 설정 (슬라이더도 반영)
            sliderBGM.value = isMuted ? 0f : mixerManager.GetVolume(EAudioMixerType.BGM);
            sliderSFX.value = isMuted ? 0f : mixerManager.GetVolume(EAudioMixerType.SFX);
        });

        // 4. 뒤로가기 버튼
        BackButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.UISFX("UI_Click", 1f);
            audioSettingsUI.SetActive(false); // UI 비활성화
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseScreen.GetComponent<pauseScreen>().isScreenPause)
        {
            audioSettingsUI.SetActive(true);
        }
    }
}
