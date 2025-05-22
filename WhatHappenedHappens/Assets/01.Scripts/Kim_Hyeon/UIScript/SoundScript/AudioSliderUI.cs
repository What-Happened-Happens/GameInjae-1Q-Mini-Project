using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioSliderUI : AudioManager /*, IPointerDownHandler*/
{
    [Header("BGM")]
    [SerializeField] private Slider _BGMAudioSlider;
    [SerializeField] private Text _BGMaudioText;

    [Header("SFX")]
    [SerializeField] private Slider _SFXAudioSlider;
    [SerializeField] private Text _SFXaudioText;

    private void OnEnable()
    {
        _BGMAudioSlider.onValueChanged.AddListener(OnBGMValueChanged);
        _SFXAudioSlider.onValueChanged.AddListener(OnSFXValueChanged);

        // 초기 텍스트 설정
        OnBGMValueChanged(_BGMAudioSlider.value);
        OnSFXValueChanged(_SFXAudioSlider.value);
    }

    private void OnDisable()
    {
        _BGMAudioSlider.onValueChanged.RemoveListener(OnBGMValueChanged);
        _SFXAudioSlider.onValueChanged.RemoveListener(OnSFXValueChanged);
    }

    private void OnBGMValueChanged(float value)
    {
        _BGMaudioText.text = value <= 0f ? "X" : $"{Mathf.RoundToInt(value * 100)}%";
    }

    private void OnSFXValueChanged(float value)
    {
        _SFXaudioText.text = value <= 0f ? "X" : $"{Mathf.RoundToInt(value * 100)}%";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Slider clicked");
    }
}
