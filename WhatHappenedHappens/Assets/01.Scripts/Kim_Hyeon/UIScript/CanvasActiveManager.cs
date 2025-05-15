using UnityEngine;

public class CanvasActiveManager : MonoBehaviour
{
    // ����, ��� �ּ��� ����� ���� 
    [SerializeField] GameObject UIObjectPrefab;  // ���� ������ UI Prefab
    private RectTransform _cardKeyRect;
    public bool IsCanvasActived { get; set; } = false;

    void Start()
    {
        if (UIObjectPrefab == null)
        {
            Debug.LogError($"UIManager: {UIObjectPrefab.name} ������Ʈ�� �Ҵ�Ǿ����� �ʽ��ϴ�.", this);
            enabled = false;
            return;
        }

        _cardKeyRect = UIObjectPrefab.GetComponent<RectTransform>();
        if (_cardKeyRect == null)
        {
            Debug.LogError($"UIManager: {UIObjectPrefab.name} RectTransform�� �Ҵ�Ǿ����� �ʽ��ϴ�.", this);
            enabled = false;
            return;
        }

        // Start hidden
        UIObjectPrefab.SetActive(false);
    }
  
    public void SetUIActive(bool activestate) => UIObjectPrefab.SetActive(activestate);


    // �÷��̾ ī��Ű ������Ʈ�� �Ծ��� �� ȣ���� Ȱ�� ���� ���¸�  true�� ��ȯ��
    public void ShowUIObject(bool isActived)
    {
        if (isActived == false) return;
                
        if (isActived) //  UIObject�� ����� �������� ��
        {
            Debug.Log($"������Ʈ �� ���� Ȱ��ȭ : {isActived}");
            SetUIActive(true); // uiPrefab�� Ȱ��ȭ 
        }
    }
    // �÷��̾ ī��Ű ������Ʈ�� �Ծ��� �� ȣ���� Ȱ�� ���� ���¸�  false��  ��ȯ��
    public void HideUIObject(bool isActived)
    {
        if (isActived == true) return;

        if (isActived == false) // UIObject�� ����� ������ ��
        {
            Debug.Log($"������Ʈ �� ���� ��Ȱ��ȭ : {isActived}");
            SetUIActive(false); // uiPrefab�� ��Ȱ��ȭ 
        }
    }
}
