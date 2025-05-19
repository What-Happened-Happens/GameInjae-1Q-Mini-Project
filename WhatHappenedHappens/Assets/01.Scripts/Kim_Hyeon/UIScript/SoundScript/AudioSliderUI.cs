using UnityEngine;
using UnityEngine.EventSystems;

public class AudioSliderUI : AudioManager, IPointerDownHandler
{
    private void OnEnable()
    {
        AudioSlider.onValueChanged.AddListener(onAudioValueChanged);
        onAudioValueChanged(AudioSlider.value);
    }
    private void OnDisable()
    {
        AudioSlider.onValueChanged.RemoveListener(onAudioValueChanged);
    }
    public async void onAudioValueChanged(float value)
    {
        if (_isMute) return;
                
        if (value >= 0f && value <= 100f)
        {
            _currentSliderValue = value / 100f;

            AudioSource.volume = _currentSliderValue;
            AudioValueText.text = AudioSource.volume <= 0f ? "X" : $"{Mathf.RoundToInt(value)}%";

            //  이전 값에 현재 값을 로드 
            _PrevSoundValue = await AudioLoad("save_CurrentSoundValue", 0f);
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
            AudioSource.volume = 0f;
            AudioSlider.value = 0f; 
        }
       

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Slider clicked");
    }
}
