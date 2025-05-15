using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundSliderManager : MonoBehaviour
{
    [SerializeField] private Slider slider;      
    [SerializeField] private TMP_Text valueText; 

    void Start()
    {
        UpdateText(slider.value);

        // 값이 바뀔 때마다 UpdateText 호출
        slider.onValueChanged.AddListener(UpdateText);
    }

    // Update is called once per frame
    private void UpdateText(float value)
    {
        if (slider == null) return; 

        Debug.Assert(slider == null, $"Slider 객체가 비어있습니다. {slider}"); 
        valueText.text = value.ToString("0.##");
    }
    private void OnSliderDestroy()
    {
        Debug.Log($"Slider 값 해제");
        slider.onValueChanged.RemoveListener(UpdateText);
    }
}
