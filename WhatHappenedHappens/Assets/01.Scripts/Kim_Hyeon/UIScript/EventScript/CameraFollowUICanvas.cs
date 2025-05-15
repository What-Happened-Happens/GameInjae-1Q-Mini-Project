using UnityEngine;

public class CameraFollowUICanvas : MonoBehaviour
{
    [SerializeField] private Camera _targetCamera;    // 따라 움직일 mainCamera 
    [SerializeField] private Canvas _uiCanvas;
    private Vector3 _localOffset;  // 카메라에 따라갈 UI canvas 위치 

    private void Awake()
    {        
        _uiCanvas = GetComponent<Canvas>();
        _uiCanvas.renderMode = RenderMode.ScreenSpaceCamera;
       
        _localOffset = Vector2.zero;

        if (_targetCamera == null)
            _targetCamera = GetComponent<Camera>();
        _uiCanvas.worldCamera = _targetCamera;
    }

    private void Update()
    {
        transform.position = _targetCamera.transform.position + _localOffset;
    }

}
