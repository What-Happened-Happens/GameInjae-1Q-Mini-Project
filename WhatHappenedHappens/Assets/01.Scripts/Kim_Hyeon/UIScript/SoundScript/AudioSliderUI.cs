using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioSliderUI : AudioManager, IPointerDownHandler
{
    private bool isPlaying = false;

    private void Start()
    {
        AudioSlider.onValueChanged.AddListener(AudioValueChanged); 
    }
    private void OnDisable()
    {
        AudioSlider.onValueChanged.RemoveListener(AudioValueChanged);
    }
    public async void AudioValueChanged(float value)
    {
        if (!_isPlaying) return;

        var sound = SoundValueLoad("save_CurrentSoundValue");

        if (sound != null)
        {
            if(_currentSliderValue != _PrevSoundValue) // 현재 값이 지금 값과 다를 떄 
            {
                isMute(true);
                Debug.Log($"음향을 다시 플레이 합니다. 현 상태 : {_isPlaying}");
                float AudioVolume = await SoundValueLoad("save_CurrentSoundValue");
                _PrevSoundValue = AudioVolume; // 현재 볼륨값을 이전 볼륨값으로 저장 
                               
                AudioSource.volume = _PrevSoundValue;
                AudioSlider.value = AudioSource.volume;
                soundvalueText.text = AudioSlider.value.ToString();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Slider clicked");
    }
}
