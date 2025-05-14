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
    }

    // Update is called once per frame
    void Update()
    {
       
        if (uiTarget == null || player == null) return;
        Debug.Assert(uiTarget != null, "UI ������Ʈ�� ����ֽ��ϴ�!");
        Debug.Assert(player != null, "�÷��̾� ������Ʈ�� ����ֽ��ϴ�!");
        Vector2 screenPos = mainCamera.WorldToScreenPoint(player.position);

        if (RectTransformUtility.RectangleContainsScreenPoint(uiTarget, screenPos, null))
        {
            Debug.Log("UI �̹����� ������Ʈ�� ���� ��ġ�� ����! (�浹 ����)");
        }

    }
}
