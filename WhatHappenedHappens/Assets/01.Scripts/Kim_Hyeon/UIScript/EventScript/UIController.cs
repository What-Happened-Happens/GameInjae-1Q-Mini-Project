using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : UIHelper
{
    [Header("Player")]
    public GameObject Player;
    public GameObject target;

    [Header("Icon")]
    public Camera uiCamera;
    public Canvas uiCanvas;

    [Header("Icon List")]
    public List<GameObject> icons = new List<GameObject>();

    [Header("Setting")]
    [SerializeField] private float minDistance = 0.5f;
    [SerializeField] private float maxDistance = 5f;
    private float offset = 2f;
    private float duration = 3f; // �������� ������� �ð� 

    // test code 
    private bool isIntrective = true; // �׽�Ʈ ������ true  

    private void Start()
    {
        //iconInstance = Instantiate(iconInstance, uiCanvas.transform);
        //iconInstance.gameObject.SetActive(false);
        //UIIconUpdate();
       
    }

    private void Update()
    {
        UIIconUpdate();
    }

    public void UIIconUpdate()
    {
        // �Ÿ� ���ϴ� �� Vector2 
        Vector2 playerPos = Player.transform.position;
        Vector2 targetPos = target.transform.position;

        // �Ÿ� ���ϱ� 
        float distance = Vector2.Distance(playerPos, targetPos);
        Debug.Log($"�÷��̾�� Ÿ�� �Ÿ�: {distance}");

        if (distance <= maxDistance && distance >= minDistance)
        {
            ShowIcon(target);
        }
        else
        {
            UIIconHide();
        }

    }

    public void ShowIcon(GameObject obj)
    {
        for (int i = 0; i < icons.Count; i++)
        {
            if (icons[i].activeSelf == true) return;  

            icons[i].SetActive(true); 
        }
        Debug.Log($"�������� ���Դϴ�.");

        Vector2 worldPos = obj.transform.position + Vector3.down * offset;
        Vector3 screenPos = uiCamera.WorldToScreenPoint(worldPos);

    }



    public void UIIconHide()
    {
        // ������ ����� 
        iconInstance.gameObject.SetActive(false);
    }
}
