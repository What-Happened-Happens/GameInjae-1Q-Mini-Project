using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTF : TrueFalse
{
    [Header("TF")]

    private MovingPlatform movingPlatform;

    private int playerCount = 0; // 충돌 중인 플레이어 수

    private void Start()
    {
    }

    // 버튼 눌림
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount++;
            isTrue = true;

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
            if (playerCount < 0) playerCount = 0;


            if (playerCount == 0) isTrue = false;
        }
    }
}
