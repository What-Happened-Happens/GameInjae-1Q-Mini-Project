using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Platform : MonoBehaviour
{
    public GameObject platform;

    private MovingPlatform movingPlatform;

    private void Start()
    {
        movingPlatform = platform.GetComponent<MovingPlatform>();   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            movingPlatform.alwaysOn = true; // �÷����� �׻� �۵��ϵ��� ����
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            movingPlatform.alwaysOn = false; // �÷����� �׻� �۵��ϵ��� ����
        }
    }
}
