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

        // ���� �ٲ� ������ UpdateText ȣ��
        slider.onValueChanged.AddListener(UpdateText);
    }

    // Update is called once per frame
    private void UpdateText(float value)
    {
        if (slider == null) return; 

        Debug.Assert(slider == null, $"Slider ��ü�� ����ֽ��ϴ�. {slider}"); 
        valueText.text = value.ToString("0.##");
    }
    private void OnSliderDestroy()
    {
        Debug.Log($"Slider �� ����");
        slider.onValueChanged.RemoveListener(UpdateText);
    }
}
