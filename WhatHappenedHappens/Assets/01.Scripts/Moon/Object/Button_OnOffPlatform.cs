using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_OnOffPlatform : MonoBehaviour
{
    [Header("Button On/Off")]
    public bool platformOn = false;

    [Header("Platform Objects")]
    public List<GameObject> OnplatformObjects = new List<GameObject>();
    public List<GameObject> OffplatformObjects = new List<GameObject>();


    private void Update()
    {
        if (platformOn)
        {
            // 켜야 하는 오브젝트들
            foreach (GameObject obj in OnplatformObjects)
                obj.SetActive(true);

            // 꺼야 하는 오브젝트들
            foreach (GameObject obj in OffplatformObjects)
                obj.SetActive(false);
        }
        else
        {
            foreach (GameObject obj in OnplatformObjects)
                obj.SetActive(false);

            foreach (GameObject obj in OffplatformObjects)
                obj.SetActive(true);
        }
    }

    // 버튼 눌림 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            platformOn = true; // OnOffPlatform 비활성화
        }
    }

    // 버튼 안눌림 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            platformOn = false;
        }
    }
}
