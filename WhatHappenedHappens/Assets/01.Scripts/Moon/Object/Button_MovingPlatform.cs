using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Platform : MonoBehaviour
{
    [Header("Platform")]
    public GameObject platform;

    private MovingPlatform movingPlatform;
    

    private void Start()
    {
        movingPlatform = platform.GetComponent<MovingPlatform>();
    }

    // ��ư ���� 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            movingPlatform.alwaysOn = true;
        }
    }

    // ��ư �ȴ��� 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            movingPlatform.alwaysOn = false;
        }
    }
}
