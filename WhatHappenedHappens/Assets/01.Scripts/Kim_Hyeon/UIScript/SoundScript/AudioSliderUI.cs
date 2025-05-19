using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioSliderUI : AudioManager, IPointerDownHandler
{
    [Header("AudioSlider")]
    public Slider AudioSlider;
    [Header("AudioText")]
    public TMP_Text AudioText;

    private bool isPlaying = false;

    private void Start()
    {
        AudioSlider = GetComponent<Slider>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Slider clicked");
    }
}
