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

    // ��ư ���� 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonSprite.sprite = ButtonOn;
        }
    }

    // ��ư �ȴ��� 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonSprite.sprite = ButtonOff;
        }
    }
}
