using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 추후, 모든 주석은 영어로 변경 
    [SerializeField] GameObject uiPrefab;  // 생성 관리할 UI Prefab
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
    // UI 프리팹 위치 지정 ★
    public void SetUIPos(Vector2 position) => uiPrefab.transform.position = position;
    public Vector2 GetUIPos() => uiPrefab.transform.position;

    // 플레이어가 카드키 오브젝트를 먹었을 때 호출할 SetActive를 true로 변환★
    public void ShowUIObject(bool isGetCardKey)
    {
        if (isGetCardKey == false) return;        

        if(isGetCardKey) //  UIObject의 사용이 시작했을 때
        {
            uiPrefab.SetActive(true); // uiPrefab을 활성화 
        }
    }
    public void HideUIObject(bool isGetCardKey)
    {
        if (isGetCardKey == true) return;

        if (isGetCardKey == false) // UIObject의 사용이 끝났을 때
        {
            uiPrefab.SetActive(false); // uiPrefab을 비활성화 
        }
    }



}
