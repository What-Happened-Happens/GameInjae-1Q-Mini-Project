using UnityEngine;

public class CameraFade : MonoBehaviour
{
    [Header("PlayerPrefab")]
    [SerializeField] private GameObject playerPrefab; // �߽��� �� �÷��̾� ������ 
    [SerializeField] private Camera CameraPrefab;     // �÷��̾ ���� �ٴ� ī�޶� 

    private bool isClear = false;  // �������� Ŭ���� ���� Ȯ�� 

    private void Start()
    {
        // ī�޶��� ��ġ�� �÷��̾��� ��ġ�� �߽��� ��´�. 
        CameraPrefab.gameObject.transform.position = playerPrefab.transform.position; 

    }



}
