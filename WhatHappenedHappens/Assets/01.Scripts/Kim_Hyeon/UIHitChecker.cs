using UnityEngine;

public class UIHitChecker : MonoBehaviour
{
    // 추후 모듈화를 위해 파라메터로 전환할 변수 
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
        Debug.Assert(uiTarget != null, "UI 오브젝트가 비어있습니다!");
        Debug.Assert(player != null, "플레이어 오브젝트가 비어있습니다!");
        Vector2 screenPos = mainCamera.WorldToScreenPoint(player.position);

        if (RectTransformUtility.RectangleContainsScreenPoint(uiTarget, screenPos, null))
        {
            Debug.Log("UI 이미지와 오브젝트가 같은 위치에 있음! (충돌 판정)");
        }

    }
}
