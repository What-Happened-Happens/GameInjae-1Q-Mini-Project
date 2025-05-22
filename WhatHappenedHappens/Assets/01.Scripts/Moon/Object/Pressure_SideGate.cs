using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_SideGate : MonoBehaviour
{
    [Header("Button On/Off")]
    public bool sideGateOpen = false;

    [Header("Objects")]
    public GameObject sideGate;

    private bool isOpen = false; // 문 열림 여부
    private int playerCount = 0; // 충돌 중인 플레이어 수

    private Animator anim;
    private Collider2D col;

    private void Start()
    {
        anim = sideGate.GetComponent<Animator>();
        col = sideGate.GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (sideGateOpen && !isOpen)
        {
            // 열린 상태로 전환
            anim.SetTrigger("Open");
            col.isTrigger = true;
            isOpen = true;
        }
        else if (!sideGateOpen && isOpen)
        {
            // 닫힌 상태로 전환
            anim.SetTrigger("Close");
            col.isTrigger = false;
            isOpen = false;
        }
    }

    // 버튼 위에 플레이어 올라감
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount++;
            sideGateOpen = true;
        }
    }

    // 버튼에서 플레이어 내려감
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount--;
            if (playerCount <= 0)
            {
                playerCount = 0;
                sideGateOpen = false;
            }
        }
    }

}
