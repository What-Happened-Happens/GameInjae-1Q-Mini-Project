using UnityEngine;

public class UIManager : MonoBehaviour
{
    // ����, ��� �ּ��� ����� ���� 
    [SerializeField] GameObject uiPrefab;  // ���� ������ UI Prefab    
    public bool IsGetCardKey { get; set; } = false; 

    void Start()
    {
        uiPrefab.SetActive(false);        // ���� �� ������ uiPrefab�� ��Ȱ��ȭ�� ����. 
    }

    // �÷��̾ ī��Ű ������Ʈ�� �Ծ��� �� ȣ���� SetActive�� true�� ��ȯ��
    public void PlayerGetCardKey(bool isGetCardKey)
    {
        if (isGetCardKey == false) return;
        Debug.Assert(isGetCardKey != true, $"ī��Ű�� ���� isGetCardKey ��� �� : {isGetCardKey}");

        if(isGetCardKey) // ī��Ű�� �Ծ��� ��, 
        {
            uiPrefab.SetActive(true); // uiPrefab�� Ȱ��ȭ 
        }
    }
    


}
