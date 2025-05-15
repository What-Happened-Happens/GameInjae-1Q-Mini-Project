using UnityEngine;

public class CanvasActiveManager : MonoBehaviour
{
    // 추후, 모든 주석은 영어로 변경 
    [SerializeField] GameObject UIObjectPrefab;  // 생성 관리할 UI Prefab
    private RectTransform _cardKeyRect;
    public bool IsCanvasActived { get; set; } = false;

    void Start()
    {
        if (UIObjectPrefab == null)
        {
            Debug.LogError($"UIManager: {UIObjectPrefab.name} 오브젝트가 할당되어있지 않습니다.", this);
            enabled = false;
            return;
        }

        _cardKeyRect = UIObjectPrefab.GetComponent<RectTransform>();
        if (_cardKeyRect == null)
        {
            Debug.LogError($"UIManager: {UIObjectPrefab.name} RectTransform이 할당되어있지 않습니다.", this);
            enabled = false;
            return;
        }

        // Start hidden
        UIObjectPrefab.SetActive(false);
    }
  
    public void SetUIActive(bool activestate) => UIObjectPrefab.SetActive(activestate);


    // 플레이어가 카드키 오브젝트를 먹었을 때 호출할 활성 여부 상태를  true로 변환★
    public void ShowUIObject(bool isActived)
    {
        if (isActived == false) return;
                
        if (isActived) //  UIObject의 사용이 시작했을 때
        {
            Debug.Log($"오브젝트 씬 내에 활성화 : {isActived}");
            SetUIActive(true); // uiPrefab을 활성화 
        }
    }
    // 플레이어가 카드키 오브젝트를 먹었을 때 호출할 활성 여부 상태를  false로  변환★
    public void HideUIObject(bool isActived)
    {
        if (isActived == true) return;

        if (isActived == false) // UIObject의 사용이 끝났을 때
        {
            Debug.Log($"오브젝트 씬 내에 비활성화 : {isActived}");
            SetUIActive(false); // uiPrefab을 비활성화 
        }
    }
}
