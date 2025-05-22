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

    [Tooltip("������ ����Ʈ. ��� �����ܵ� ������")]
    [Header("Icon List")]
    public List<GameObject> icons = new List<GameObject>();

    [Header("Setting")]
    [SerializeField] private float minDistance = 0f;
    [SerializeField] private float maxDistance = 0.5f;
    private float offset = 2f;
    private float duration = 3f; // �������� ������� �ð� 

    [Header("Use TargetIcon")]
    public GameObject targetIcon;

    // ���� ĳ��
    private GameObject currentIcon;

    // �÷��̾�� Ÿ�� �Ÿ�
    float distance = 0f;

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

        if (distance >= minDistance && distance <= maxDistance)
        {
            Debug.Log($"Show : �÷��̾�� Ÿ���� �Ÿ��� {distance} �Դϴ�.");
            ShowIcon(targetIcon); // ������ ���̱�
            MoveUIShow(targetIcon); // �̵� ������ ���̱� 
        }
        else
        {
            Debug.Log($"Hide : �÷��̾�� Ÿ���� �Ÿ��� {distance} �����Դϴ�.");
            HideIcon();
            MoveUIHide(targetIcon);// �̵� ������ �����
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
        Debug.Log($"�������� ���Դϴ�.");

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
