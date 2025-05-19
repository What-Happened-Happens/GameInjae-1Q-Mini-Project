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
            // �Ѿ� �ϴ� ������Ʈ��
            foreach (GameObject obj in OnplatformObjects)
                obj.SetActive(true);

            // ���� �ϴ� ������Ʈ��
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

    // ��ư ���� 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            platformOn = true; // OnOffPlatform ��Ȱ��ȭ
        }
    }

    // ��ư �ȴ��� 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            platformOn = false;
        }
    }
}
