using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Platform : MonoBehaviour
{
    public GameObject platform;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger");
            platform.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            platform.SetActive(false);
        }
    }
}
