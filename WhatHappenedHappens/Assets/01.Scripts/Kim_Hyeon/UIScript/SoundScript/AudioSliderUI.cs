using UnityEngine;
using UnityEngine.EventSystems;

public class AudioSliderUI : AudioManager, IPointerDownHandler
{
    private void OnEnable()
    {
        // BGM
        _BGMAudioSlider.onValueChanged.AddListener(onBGMValueChangedAsync);
        onBGMValueChangedAsync(_BGMAudioSlider.value);
        //SFX
        _SFXAudioSlider.onValueChanged.AddListener(OnSFXSliderChangedAsync);
        OnSFXSliderChangedAsync(_SFXAudioSlider.value);

    }
    private void OnDisable()
    {
        // BGM
        _BGMAudioSlider.onValueChanged.RemoveListener(onBGMValueChangedAsync);
        // SFX
        _SFXAudioSlider.onValueChanged.RemoveListener(OnSFXSliderChangedAsync);
    }

    public async void OnSFXSliderChangedAsync(float value)
    {
        if (_isMute) return;

        float normalized = value / 100f;     

        await AudioSave("save_SFXSoundVolume", normalized);

        SFXAudioManager.Instance.volumeScale = normalized;

        foreach (var entry in SFXAudioManager.Instance.stateClips)
        {
            if (entry.targetOutput != null)
            {
                entry.targetOutput.volume = _currentSFXsliderValue;
                _SFXaudioText.text = _BGMaudioSource.volume <= 0f ? "X" : $"{Mathf.RoundToInt(value)}%";
                entry.targetOutput.volume = normalized;
                if (!_isMute && _currentSFXsliderValue > 0f)
                {
                    isMute(true);
                    Debug.Log($"음향을 다시 플레이 합니다.");
                }
            }
            else if (value == 0f)
            {
                isMute(false);               
                // SFX 
                _SFXAudioSlider.value = 0f;
                entry.targetOutput.volume = 0f;
            }

        }
    }

    public async void onBGMValueChangedAsync(float value)
    {
        if (_isMute) return;

        if (value >= 0f && value <= 100f)
        {
            _currentSliderValue = value / 100f;

            _BGMaudioSource.volume = _currentSliderValue;
            _BGMaudioText.text = _BGMaudioSource.volume <= 0f ? "X" : $"{Mathf.RoundToInt(value)}%";

            //  이전 값에 현재 값을 로드 
            _PrevBGMSoundValue = await AudioLoad("save_BGMSoundVolume", 0f);
            Debug.Log($"현재 음향 값을 다시 로드했습니다. ");
            Debug.Log($"음향을 다시 플레이 합니다.");

            if (!_isMute && _currentSliderValue > 0f)
            {
                isMute(true);
                Debug.Log($"음향을 다시 플레이 합니다.");
            }
        }
        else if (value == 0f)
        {
            isMute(false);
            _BGMaudioSource.volume = 0f;
            _BGMAudioSlider.value = 0f;

        }

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Slider clicked");
    }
}
