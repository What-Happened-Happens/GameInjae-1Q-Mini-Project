using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOnOff : MonoBehaviour
{
    [Header("Button On/Off")]
    public Sprite ButtonOn;
    public Sprite ButtonOff;

    private SpriteRenderer ButtonSprite;

    private int playerCount = 0; // 충돌 중인 플레이어 수

    private void Start()
    { 
        ButtonSprite = GetComponent<SpriteRenderer>();
    }

    // 버튼 눌림 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount++;
            ButtonSprite.sprite = ButtonOn;
        }
    }

    // 버튼 안눌림 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount--;
            if (playerCount == 0) ButtonSprite.sprite = ButtonOff;
        }
    }
}
