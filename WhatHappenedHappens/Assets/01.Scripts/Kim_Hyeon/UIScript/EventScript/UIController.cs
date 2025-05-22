using Assets._01.Scripts.Kim_Hyeon.UIScript.SceneScript;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : UIHelper
{
    [Header("Player & targetObject")]
    public Rigidbody2D Player;
    public Transform target;

    [Tooltip("아이콘 리스트. 모든 아이콘들 관리용")]
    [Header("Icon List")]
    public List<GameObject> icons = new List<GameObject>();

    [Header("Setting")]
    [SerializeField] private float minDistance = 0f;
    [SerializeField] private float maxDistance = 0.5f;
    private float offset = 2f;
    private float duration = 3f; // 아이콘이 사라지는 시간 

    [Header("Use TargetIcon")]
    public GameObject targetIcon;

    // 내부 캐시
    private GameObject currentIcon;

    // 플레이어와 타겟 거리
    float distance = 0f;

    private void Awake()
    {
        foreach (var icon in icons)
            icon.gameObject.SetActive(false);
    }

    private void Update()
    {
        // 거리 구하는 용 Vector2 
        Vector2 playerPos = Player.transform.position;
        Vector2 targetPos = target.transform.position;
        // 거리 구하기 
        distance = Vector2.Distance(playerPos, targetPos);

        UIIconUpdate();
    }


    public void UIIconUpdate()
    {

        if (distance >= minDistance && distance <= maxDistance)
        {
            Debug.Log($"Show : 플레이어와 타겟의 거리가 {distance} 입니다.");
            ShowIcon(targetIcon); // 아이콘 보이기
            MoveUIShow(targetIcon); // 이동 아이콘 보이기 
        }
        else
        {
            Debug.Log($"Hide : 플레이어와 타겟의 거리가 {distance} 사이입니다.");
            HideIcon();
            MoveUIHide(targetIcon);// 이동 아이콘 숨기기
        }

    }
    public void MoveUIShow(GameObject icon)
    {
        if (icon.name == icons[0].name)
            icons[0].SetActive(true);
    }
    public void MoveUIHide(GameObject icon)
    {
        if (icon.name == icons[0].name)
            icons[0].SetActive(false);
    }

    public void ShowIcon(GameObject iconGroup)
    {
        Debug.Log($"아이콘을 보입니다.");

        if (currentIcon == iconGroup && iconGroup.activeSelf)
        {
            UpdatePosition(iconGroup);
            return;
        }

        foreach (var go in icons)
            go.SetActive(false);

        iconGroup.SetActive(true);
        currentIcon = iconGroup;

        UpdatePosition(iconGroup);
    }
    void HideIcon()
    {
        if (currentIcon != null)
        {
            currentIcon.SetActive(false);
            currentIcon = null;
        }
    }

    void UpdatePosition(GameObject iconGroup)
    {
        Vector3 worldPos = target.position + Vector3.up * offset;
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(worldPos);

        Vector2 anchored = viewportPos;

        var rt = iconGroup.GetComponent<RectTransform>();
        rt.anchoredPosition = anchored;
    }
}
