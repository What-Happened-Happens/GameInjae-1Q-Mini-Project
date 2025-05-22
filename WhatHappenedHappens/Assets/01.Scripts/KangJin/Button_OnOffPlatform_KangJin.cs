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

    private int playerCount = 0; // �浹 ���� �÷��̾� ��

    private void Update()
    {
        //��������
        if (platformOn)
        {
            // �Ѿ� �ϴ� ������Ʈ��
            foreach (GameObject obj in OnplatformObjects)
            {
                obj.SetActive(true);
                OnOff_Platform_Effect ope = obj.GetComponent<OnOff_Platform_Effect>();
                if (ope != null)
                {
                    ope.TriggerCreate();
                }
            }

            // ���� �ϴ� ������Ʈ��
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
        else //�ݴ���
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

    // ��ư ���� 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount++;
            platformOn = true; // OnOffPlatform ��Ȱ��ȭ
        }
    }

    // ��ư �ȴ��� 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount--;
            if (playerCount == 0) platformOn = false;
        }
    }
}
