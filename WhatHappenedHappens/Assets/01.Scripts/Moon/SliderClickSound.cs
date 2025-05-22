using UnityEngine;
using UnityEngine.EventSystems;

public class SliderClickSound : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.Instance.UISFX("UI_Click", 1f);
    }
}