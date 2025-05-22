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
    private float duration = 3f; // 아이콘이 사라지는 시간 

    // test code 
    private bool isIntrective = true; // 테스트 용으로 true  

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
        // 거리 구하는 용 Vector2 
        Vector2 playerPos = Player.transform.position;
        Vector2 targetPos = target.transform.position;

        // 거리 구하기 
        float distance = Vector2.Distance(playerPos, targetPos);
        Debug.Log($"플레이어와 타겟 거리: {distance}");

        if (distance >= minDistance && distance <= maxDistance)
        {
            Debug.Log($"플레이어와 타겟의 거리가 {minDistance} ~ {maxDistance} 사이입니다."); 
            Debug.Log($"아이콘을 보입니다."); 
        }
        else
        {
            Debug.Log($"플레이어와 타겟의 거리가 {minDistance} ~ {maxDistance} 사이입니다.");
            Debug.Log($"아이콘을 숨깁니다.");
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
        // 아이콘 숨기기 
        for (int i = 0; i < icons.Count; i++)
        {
            if (icons[i].activeSelf == false) return;

            icons[i].SetActive(false);
            Debug.Log($"아이콘을 숨깁니다.");

        }
    }
}
