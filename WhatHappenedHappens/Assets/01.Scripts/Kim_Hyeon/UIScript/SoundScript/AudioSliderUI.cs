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
            if(_currentSliderValue != _PrevSoundValue) // ���� ���� ���� ���� �ٸ� �� 
            {
                isMute(true);
                Debug.Log($"������ �ٽ� �÷��� �մϴ�. �� ���� : {_isPlaying}");
                float AudioVolume = await SoundValueLoad("save_CurrentSoundValue");
                _PrevSoundValue = AudioVolume; // ���� �������� ���� ���������� ���� 
                               
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
