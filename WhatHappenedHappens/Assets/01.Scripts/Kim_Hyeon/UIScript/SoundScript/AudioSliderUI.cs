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
                    Debug.Log($"������ �ٽ� �÷��� �մϴ�.");
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

            //  ���� ���� ���� ���� �ε� 
            _PrevBGMSoundValue = await AudioLoad("save_BGMSoundVolume", 0f);
            Debug.Log($"���� ���� ���� �ٽ� �ε��߽��ϴ�. ");
            Debug.Log($"������ �ٽ� �÷��� �մϴ�.");

            if (!_isMute && _currentSliderValue > 0f)
            {
                isMute(true);
                Debug.Log($"������ �ٽ� �÷��� �մϴ�.");
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
