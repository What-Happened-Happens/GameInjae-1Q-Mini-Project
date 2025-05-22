using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIiconController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player & targetObject")]
    public RectTransform canvasRect;
    public Rigidbody2D Player;
    public Transform target;

    [Tooltip("아이콘 리스트. 모든 아이콘들 관리용")]
    [Header("Icon List")]
    public List<GameObject> icons = new List<GameObject>();

    [Header("Setting")]
    [SerializeField] private float minDistance = 0f;
    [SerializeField] private float maxDistance = 1f;
    private float offset = 2f;

    [Header("Use TargetIcon")]
    public GameObject targetIcon;

    // 내부 캐시
    private GameObject currentIcon;

    // 플레이어와 타겟 거리
    float distance = 0f;
    public void SetIcon(GameObject obj) { targetIcon = obj; }

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

        if (distance <= minDistance )
        {
            Debug.Log($"Show : 플레이어와 타겟의 거리가 {distance} 입니다.");
            ShowIcon(targetIcon); // 아이콘 보이기
        }
        else if ( distance >= maxDistance)
        {
            Debug.Log($"Hide : 플레이어와 타겟의 거리가 {distance} 사이입니다.");
            HideIcon();
        }
        else
        {
            return; 
        }

    }

    public void ShowIcon(GameObject iconGroup)
    {
        Debug.Log($"{iconGroup.name}아이콘을 보입니다.");

        if (currentIcon == iconGroup && iconGroup.activeSelf)
        {
            UpdatePosition(iconGroup);
            return;
        }

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

        float x = (viewportPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x);
        float y = (viewportPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y);
        Vector2 anchored = new Vector2(x, y);

        var rt = iconGroup.GetComponent<RectTransform>();
        rt.anchoredPosition = anchored;

    }
}

