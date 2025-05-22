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
    public Canvas uiCanvas;
    private RectTransform canvasRect;

    [Header("Icon List")]
    public List<GameObject> icons = new List<GameObject>();

    [Header("Setting")]
    [SerializeField] private float minDistance = 0.5f;
    [SerializeField] private float maxDistance = 3f;
    private float offset = 2f;
    private float duration = 3f; // �������� ������� �ð� 

    // test code 
    private bool isIntrective = true; // �׽�Ʈ ������ true  

    private void Awake()
    {
        canvasRect = uiCanvas.GetComponent<RectTransform>();
        foreach (var icon in icons)
            icon.gameObject.SetActive(false);
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

        if (distance >= minDistance && distance <= maxDistance)
        {
            Debug.Log($"�÷��̾�� Ÿ���� �Ÿ��� {minDistance} ~ {maxDistance} �����Դϴ�."); 
            Debug.Log($"�������� ���Դϴ�."); 
        }
        else
        {
            Debug.Log($"�÷��̾�� Ÿ���� �Ÿ��� {minDistance} ~ {maxDistance} �����Դϴ�.");
            Debug.Log($"�������� ����ϴ�.");
            UIIconHide();
        }

    }

    public void ShowIcon(GameObject obj)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position);

        string wantName = obj.name;

        for (int i = 0; i < icons.Count; i++)
        {
            bool show = icons[i].name == wantName;
            icons[i].SetActive(show);
            if (show)
                icons[i].transform.position = screenPos;
        }


    }



    public void UIIconHide()
    {
        // ������ ����� 
        for (int i = 0; i < icons.Count; i++)
        {
            if (icons[i].activeSelf == false) return;

            icons[i].SetActive(false);
            Debug.Log($"�������� ����ϴ�.");

        }
    }
}
