using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Platform : MonoBehaviour
{
    [Header("Platform")]
    public GameObject platform;

    [Header("Button On/Off")]
    public Sprite ButtonOn;
    public Sprite ButtonOff;

    private MovingPlatform movingPlatform;
    private SpriteRenderer ButtonSprite;
    

    private void Start()
    {
        movingPlatform = platform.GetComponent<MovingPlatform>();
        ButtonSprite = GetComponent<SpriteRenderer>();
    }

    // ��ư ���� 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            movingPlatform.alwaysOn = true;
            ButtonSprite.sprite = ButtonOn;
        }
    }

    // ��ư �ȴ��� 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            movingPlatform.alwaysOn = false;
            ButtonSprite.sprite = ButtonOff;
        }
    }
}
