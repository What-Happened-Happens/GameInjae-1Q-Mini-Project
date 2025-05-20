using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Platform : MonoBehaviour
{
    [Header("Platform")]
    public GameObject platform;

    private MovingPlatform movingPlatform;

    private int playerCount = 0; // 충돌 중인 플레이어 수

    private void Start()
    {
        movingPlatform = platform.GetComponent<MovingPlatform>();
    }

    // 버튼 눌림 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount++;
            movingPlatform.alwaysOn = true;

            Debug.Log("playerCount : " + playerCount);
        }
    }

    // 버튼 안눌림 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount--;
            Debug.Log("playerCount : " + playerCount);
            if (playerCount < 0)  playerCount = 0;


            if (playerCount == 0)  movingPlatform.alwaysOn = false;
        }
    }
}
