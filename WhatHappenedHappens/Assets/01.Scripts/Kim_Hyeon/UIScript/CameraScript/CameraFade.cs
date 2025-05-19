using UnityEngine;

public class CameraFade : MonoBehaviour
{
    [Header("PlayerPrefab")]
    [SerializeField] private GameObject playerPrefab; // 중심이 될 플레이어 프리팹 
    [SerializeField] private Camera CameraPrefab;     // 플레이어를 따라 다닐 카메라 

    private bool isClear = false;  // 스테이지 클리어 여부 확인 

    private void Start()
    {
        // 카메라의 위치를 플레이어의 위치로 중심을 잡는다. 
        CameraPrefab.gameObject.transform.position = playerPrefab.transform.position; 

    }



}
