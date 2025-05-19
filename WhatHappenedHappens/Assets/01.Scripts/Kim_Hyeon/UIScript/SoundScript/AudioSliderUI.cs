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

            //  ���� ���� ���� ���� �ε� 
            _PrevSoundValue = await AudioLoad("save_CurrentSoundValue", 0f);
            Debug.Log($"���� ���� ���� �ٽ� �ε��߽��ϴ�. ");
            Debug.Log($"������ �ٽ� �÷��� �մϴ�.");

            //SFX 
            _SFXaudioSource.volume = _currentSliderValue;
            _SFXaudioText.text = _SFXaudioSource.volume <= 0f ? "X" : $"{Mathf.RoundToInt(value)}%";

            //  ���� ���� ���� ���� �ε� 
            _PrevSFXSoundValue = await AudioLoad("save_CurrentSFXaudio", 0f);
            Debug.Log($"���� SFX���� ���� �ٽ� �ε��߽��ϴ�. ");
            Debug.Log($"SFX ������ �ٽ� �÷��� �մϴ�.");

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
