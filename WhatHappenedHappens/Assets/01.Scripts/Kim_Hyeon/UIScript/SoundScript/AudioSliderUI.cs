using UnityEngine;
using UnityEngine.EventSystems;

public class AudioSliderUI : AudioManager, IPointerDownHandler
{
    private void OnEnable()
    {
        // BGM
        _BGMAudioSlider.onValueChanged.AddListener(onAudioValueChanged);
        onAudioValueChanged(_BGMAudioSlider.value);
        //SFX
        _SFXAudioSlider.onValueChanged.AddListener(onAudioValueChanged);
        onAudioValueChanged(_SFXAudioSlider.value);

    }
    private void OnDisable()
    {
        _BGMAudioSlider.onValueChanged.RemoveListener(onAudioValueChanged);
    }
    public async void onAudioValueChanged(float value)
    {
        if (_isMute) return;
                
        if (value >= 0f && value <= 100f)
        {
            _currentSliderValue = value / 100f;

            _BGMaudioSource.volume = _currentSliderValue;
            _BGMaudioText.text = _BGMaudioSource.volume <= 0f ? "X" : $"{Mathf.RoundToInt(value)}%";

            //  이전 값에 현재 값을 로드 
            _PrevSoundValue = await AudioLoad("save_CurrentSoundValue", 0f);
            Debug.Log($"현재 음향 값을 다시 로드했습니다. ");
            Debug.Log($"음향을 다시 플레이 합니다.");

            //SFX 
            _SFXaudioSource.volume = _currentSliderValue;
            _SFXaudioText.text = _SFXaudioSource.volume <= 0f ? "X" : $"{Mathf.RoundToInt(value)}%";

            //  이전 값에 현재 값을 로드 
            _PrevSFXSoundValue = await AudioLoad("save_CurrentSFXaudio", 0f);
            Debug.Log($"현재 SFX음향 값을 다시 로드했습니다. ");
            Debug.Log($"SFX 음향을 다시 플레이 합니다.");

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

            // SFX 
            _SFXAudioSlider.value = 0f;
            _SFXaudioSource.volume = 0f;
        }      

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Slider clicked");
    }
}
