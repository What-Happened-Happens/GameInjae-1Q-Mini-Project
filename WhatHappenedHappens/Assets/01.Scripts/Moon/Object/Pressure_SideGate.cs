using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_SideGate : MonoBehaviour
{
    [Header("Button On/Off")]
    public bool sideGateOpen = false;

    [Header("Objects")]
    public GameObject sideGate;

    private bool isOpen = false; // �� ���� ����
    private int playerCount = 0; // �浹 ���� �÷��̾� ��

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
            // ���� ���·� ��ȯ
            anim.SetTrigger("Open");
            col.isTrigger = true;
            isOpen = true;
        }
        else if (!sideGateOpen && isOpen)
        {
            // ���� ���·� ��ȯ
            anim.SetTrigger("Close");
            col.isTrigger = false;
            isOpen = false;
        }
    }

    // ��ư ���� �÷��̾� �ö�
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount++;
            sideGateOpen = true;
        }
    }

    // ��ư���� �÷��̾� ������
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
