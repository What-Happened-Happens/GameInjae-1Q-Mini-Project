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
        if (uiTarget == null || player == null)
        {
            GameObject uiObj = GameObject.Find(uiTarget.name);
            if (uiObj != null) { uiTarget = uiObj.GetComponent<RectTransform>(); }
            
            GameObject playerObj = GameObject.FindWithTag(player.name); // 또는 정확한 이름
            if (playerObj != null) player = playerObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (uiTarget == null || player == null)
        {
            Debug.LogWarning("uiTarget 또는 player가 null입니다.");
            return;
        }

        Vector2 screenPos = mainCamera.WorldToScreenPoint(player.position);
        Debug.Log($"screenPos: {screenPos}, UI 위치: {uiTarget.position}, UI 사이즈: {uiTarget.rect.size}");

        bool isInside = RectTransformUtility.RectangleContainsScreenPoint(uiTarget, screenPos, null);  // Canvas가 Overlay일 경우
        Debug.Log("RectangleContainsScreenPoint 결과: " + isInside);


        if (isInside)
        {
            Debug.Log("UI 이미지와 오브젝트가 같은 위치에 있음! (충돌 판정)");
        }

    }
}
