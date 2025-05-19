using UnityEngine;

public class CameraFollowUICanvas : MonoBehaviour
{
    [SerializeField] private Camera _targetCamera;      // 따라 움직일 mainCamera 
    [SerializeField] private Canvas _uiCanvas;
    private Vector3 _localOffset;                       // 카메라에 따라갈 UI canvas 위치 

    private void Awake()
    {
        _uiCanvas = GetComponent<Canvas>();
        _uiCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        Debug.Log($"_uiCanvas 렌더 모드 ScreenSpaceCamera로 지정");

        _localOffset = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f));
        Debug.Log($"_uiCanvas 가 카메라를 따라갈 Canvas 위치 : [X] : {_localOffset.x}, [Y] : {_localOffset.y}");

        if (_targetCamera == null)
            _targetCamera = GetComponent<Camera>();
        _uiCanvas.worldCamera = _targetCamera;
    }

    private void Update()
    {
        transform.position = _targetCamera.transform.position + _localOffset;
    }

}
