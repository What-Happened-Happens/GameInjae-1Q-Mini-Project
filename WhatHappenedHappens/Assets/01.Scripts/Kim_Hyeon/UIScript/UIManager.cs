using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 추후, 모든 주석은 영어로 변경 
    [SerializeField] GameObject uiPrefab;  // 생성 관리할 UI Prefab
    private Vector2 UIPos;
    public bool IsGetCardKey { get; set; } = false; 

    void Start()
    {
        uiPrefab.SetActive(false);           // 시작 할 때에는 uiPrefab을 비활성화로 시작. 
        UIPos = uiPrefab.transform.position; // 시작할 때 uiPrefab 의 위치 
    }
    // UI 프리팹 위치 지정 ★
    public void SetUIPos(Vector2 position) => uiPrefab.transform.position = position;
    public Vector2 GetUIPos() => uiPrefab.transform.position;

    // 플레이어가 카드키 오브젝트를 먹었을 때 호출할 SetActive를 true로 변환★
    public void CardKeyUISetActive(bool isGetCardKey)
    {
        if (isGetCardKey == false) return;        

        if(isGetCardKey) // 카드키를 먹었을 때, 
        {
            uiPrefab.SetActive(true); // uiPrefab을 활성화 
        }
    }
   


}
