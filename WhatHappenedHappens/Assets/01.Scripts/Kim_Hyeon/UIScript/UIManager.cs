using UnityEngine;

public class UIManager : MonoBehaviour
{
    // ����, ��� �ּ��� ����� ���� 
    [SerializeField] GameObject uiPrefab;  // ���� ������ UI Prefab
    private Vector2 UIPos;
    public bool IsGetCardKey { get; set; } = false; 

    void Start()
    {
        uiPrefab.SetActive(false);           // ���� �� ������ uiPrefab�� ��Ȱ��ȭ�� ����. 
        UIPos = uiPrefab.transform.position; // ������ �� uiPrefab �� ��ġ 
    }
    // UI ������ ��ġ ���� ��
    public void SetUIPos(Vector2 position) => uiPrefab.transform.position = position;
    public Vector2 GetUIPos() => uiPrefab.transform.position;

    // �÷��̾ ī��Ű ������Ʈ�� �Ծ��� �� ȣ���� SetActive�� true�� ��ȯ��
    public void CardKeyUISetActive(bool isGetCardKey)
    {
        if (isGetCardKey == false) return;        

        if(isGetCardKey) // ī��Ű�� �Ծ��� ��, 
        {
            uiPrefab.SetActive(true); // uiPrefab�� Ȱ��ȭ 
        }
    }
   


}
