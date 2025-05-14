using UnityEngine;

public class UIHitChecker : MonoBehaviour
{
    // ���� ���ȭ�� ���� �Ķ���ͷ� ��ȯ�� ���� 
    [SerializeField] private RectTransform uiTarget;
    [SerializeField] private Transform player;
    private Camera mainCamera; 

    bool isHitiUi = false;  // now UI hit boolean 

    void Start()
    {    
        mainCamera = Camera.main;
        if (uiTarget == null || player == null)
        {
            GameObject uiObj = GameObject.Find(uiTarget.name);
            if (uiObj != null) { uiTarget = uiObj.GetComponent<RectTransform>(); }
            
            GameObject playerObj = GameObject.FindWithTag(player.name); // �Ǵ� ��Ȯ�� �̸�
            if (playerObj != null) player = playerObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (uiTarget == null || player == null)
        {
            Debug.LogWarning("uiTarget �Ǵ� player�� null�Դϴ�.");
            return;
        }

        Vector2 screenPos = mainCamera.WorldToScreenPoint(player.position);
        Debug.Log($"screenPos: {screenPos}, UI ��ġ: {uiTarget.position}, UI ������: {uiTarget.rect.size}");

        bool isInside = RectTransformUtility.RectangleContainsScreenPoint(uiTarget, screenPos, null);  // Canvas�� Overlay�� ���
        Debug.Log("RectangleContainsScreenPoint ���: " + isInside);


        if (isInside)
        {
            Debug.Log("UI �̹����� ������Ʈ�� ���� ��ġ�� ����! (�浹 ����)");
        }

    }
}
