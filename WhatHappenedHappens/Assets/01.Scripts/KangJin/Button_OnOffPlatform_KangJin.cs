using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_OnOffPlatform_KangJin : MonoBehaviour
{
    [Header("Button On/Off")]
    public bool platformOn = false;

    [Header("Platform Objects")]
    public List<GameObject> OnplatformObjects = new List<GameObject>();
    public List<GameObject> OffplatformObjects = new List<GameObject>();

    private int playerCount = 0; // 충돌 중인 플레이어 수

    private void Update()
    {
        //누르는쪽
        if (platformOn)
        {
            // 켜야 하는 오브젝트들
            foreach (GameObject obj in OnplatformObjects)
            {
                obj.SetActive(true);
                OnOff_Platform_Effect ope = obj.GetComponent<OnOff_Platform_Effect>();
                if (ope != null)
                {
                    ope.TriggerCreate();
                }
            }

            // 꺼야 하는 오브젝트들
            foreach (GameObject obj in OffplatformObjects)
            {
                OnOff_Platform_Effect ope = obj.GetComponent<OnOff_Platform_Effect>();
                if (ope != null)
                {
                    ope.TriggerDestroy();
                }
                else
                {
                    obj.SetActive(false);
                }
            }
        }
        else //반대쪽
        {
            foreach (GameObject obj in OnplatformObjects)
            {
                OnOff_Platform_Effect ope = obj.GetComponent<OnOff_Platform_Effect>();
                if (ope != null)
                {
                    ope.TriggerDestroy();
                }
                else
                {
                    obj.SetActive(false);
                }
                
            }

            foreach (GameObject obj in OffplatformObjects)
            {
                obj.SetActive(true);
                OnOff_Platform_Effect ope = obj.GetComponent<OnOff_Platform_Effect>();
                if (ope != null)
                {
                    ope.TriggerCreate();
                }
            }
        }
    }

    // 버튼 눌림 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount++;
            platformOn = true; // OnOffPlatform 비활성화
        }
    }

    // 버튼 안눌림 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount--;
            if (playerCount == 0) platformOn = false;
        }
    }
}
