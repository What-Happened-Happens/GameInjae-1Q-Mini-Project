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

    [Tooltip("������ ����Ʈ. ��� �����ܵ� ������")]
    [Header("Icon List")]
    public List<GameObject> icons = new List<GameObject>();

    [Header("Setting")]
    [SerializeField] private float minDistance = 0f;
    [SerializeField] private float maxDistance = 1f;
    private float offset = 2f;

    [Header("Use TargetIcon")]
    public GameObject targetIcon;

    // ���� ĳ��
    private GameObject currentIcon;

    // �÷��̾�� Ÿ�� �Ÿ�
    float distance = 0f;
    public void SetIcon(GameObject obj) { targetIcon = obj; }

    private void Awake()
    {
        foreach (var icon in icons)
            icon.gameObject.SetActive(false);

    }


    private void Update()
    {
        // �Ÿ� ���ϴ� �� Vector2 
        Vector2 playerPos = Player.transform.position;
        Vector2 targetPos = target.transform.position;
        // �Ÿ� ���ϱ� 
        distance = Vector2.Distance(playerPos, targetPos);

        UIIconUpdate();
    }
    public void UIIconUpdate()
    {

        if (distance <= minDistance )
        {
            Debug.Log($"Show : �÷��̾�� Ÿ���� �Ÿ��� {distance} �Դϴ�.");
            ShowIcon(targetIcon); // ������ ���̱�
        }
        else if ( distance >= maxDistance)
        {
            Debug.Log($"Hide : �÷��̾�� Ÿ���� �Ÿ��� {distance} �����Դϴ�.");
            HideIcon();
        }
        else
        {
            return; 
        }

    }

    public void ShowIcon(GameObject iconGroup)
    {
        Debug.Log($"{iconGroup.name}�������� ���Դϴ�.");

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

