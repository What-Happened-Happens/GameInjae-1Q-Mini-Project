using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundSliderManager : MonoBehaviour, IPointerDownHandler
{
    [Header("SliderTMP_Text")]
    public TMP_Text soundvalueText;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = 100;
        slider.onValueChanged.AddListener(delegate { onValueChanged(); });
    }

    public void onValueChanged()
    {
        Debug.Log($"���� �ؽ�Ʈ �� ���� {slider.value}");
        int soundvalue = (int)slider.value;
        soundvalueText.text = soundvalue.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ((IPointerDownHandler)slider).OnPointerDown(eventData);
        slider = eventData.selectedObject.GetComponent<Slider>();
        onValueChanged();
    }
}
