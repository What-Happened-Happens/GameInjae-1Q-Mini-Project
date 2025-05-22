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
    private float duration = 3f; // 아이콘이 사라지는 시간 

    // test code 
    private bool isIntrective = true; // 테스트 용으로 true  

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
        // 거리 구하는 용 Vector2 
        Vector2 playerPos = Player.transform.position;
        Vector2 targetPos = target.transform.position;

        // 거리 구하기 
        float distance = Vector2.Distance(playerPos, targetPos);
        Debug.Log($"플레이어와 타겟 거리: {distance}");

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
        Debug.Log($"아이콘이 보입니다.");

        Vector2 worldPos = obj.transform.position + Vector3.down * offset;
        Vector3 screenPos = uiCamera.WorldToScreenPoint(worldPos);

    }



    public void UIIconHide()
    {
        // 아이콘 숨기기 
        iconInstance.gameObject.SetActive(false);
    }
}
