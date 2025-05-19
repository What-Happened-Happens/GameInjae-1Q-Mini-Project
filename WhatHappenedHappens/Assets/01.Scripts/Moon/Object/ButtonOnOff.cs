using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOnOff : MonoBehaviour
{
    [Header("Button On/Off")]
    public Sprite ButtonOn;
    public Sprite ButtonOff;

    private SpriteRenderer ButtonSprite;

    private void Start()
    { 
        ButtonSprite = GetComponent<SpriteRenderer>();
    }

    // 버튼 눌림 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonSprite.sprite = ButtonOn;
        }
    }

    // 버튼 안눌림 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonSprite.sprite = ButtonOff;
        }
    }
}
