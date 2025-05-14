using UnityEngine;

public class UIManager : MonoBehaviour
{
    // ����, ��� �ּ��� ����� ���� 
    [SerializeField] GameObject uiPrefab;  // ���� ������ UI Prefab
    private RectTransform _cardKeyRect;
    public bool IsUIActived { get; set; } = false; 

    void Start()
    {
        if (uiPrefab == null)
        {
            Debug.LogError($"UIManager: {uiPrefab.name}is not assigned in the Inspector.", this);
            enabled = false;
            return;
        }

        _cardKeyRect = uiPrefab.GetComponent<RectTransform>();
        if (_cardKeyRect == null)
        {
            Debug.LogError($"UIManager: {uiPrefab.name} lacks a RectTransform.", this);
            enabled = false;
            return;
        }

        // Start hidden
        uiPrefab.SetActive(false);
    }
    // UI ������ ��ġ ���� ��
    public void SetUIPos(Vector2 position) => uiPrefab.transform.position = position;
    public Vector2 GetUIPos() => uiPrefab.transform.position;

    // �÷��̾ ī��Ű ������Ʈ�� �Ծ��� �� ȣ���� SetActive�� true�� ��ȯ��
    public void ShowUIObject(bool isGetCardKey)
    {
        if (isGetCardKey == false) return;        

        if(isGetCardKey) //  UIObject�� ����� �������� ��
        {
            uiPrefab.SetActive(true); // uiPrefab�� Ȱ��ȭ 
        }
    }
    public void HideUIObject(bool isGetCardKey)
    {
        if (isGetCardKey == true) return;

        if (isGetCardKey == false) // UIObject�� ����� ������ ��
        {
            uiPrefab.SetActive(false); // uiPrefab�� ��Ȱ��ȭ 
        }
    }



}
