using UnityEngine;
using Cinemachine;
using UnityEngine.U2D;

public class PixelPerfectZoomCinemachine : MonoBehaviour
{
    public float screenSize;
    public float screenSizeSpeed;
    public CinemachineVirtualCamera virtualCam;

    void Awake()
    {

        
    }

    void Start()
    {
        virtualCam = gameObject.GetComponent<CinemachineVirtualCamera>();
        virtualCam.m_Lens.OrthographicSize = screenSize;
        if (!virtualCam) Debug.LogError("Virtual Camera 연결 안됨!");
        screenSize = 5;
    }

    void Update()
    {
        ApplyZoom();
    }

    public void SetScreenSize(float newZoom) // 스크린의 크기 바로 조정
    {
        screenSize = Mathf.Clamp(newZoom, 1, 10);
    }

    public void GoScreenBig(float newZoom)
    {
        screenSize = Mathf.Clamp(newZoom, 1, 10);
    }

    public void GoScreenSmall(float newZoom)
    {
        screenSize = Mathf.Clamp(newZoom, 1, 10);
    }


    void ApplyZoom()
    {
        if (virtualCam == null) { 
            Debug.Log("버츄얼 켐 없음!!"); 
            return; 
        }

        virtualCam.m_Lens.OrthographicSize = screenSize;
        if (!virtualCam) Debug.LogError("Virtual Camera 연결 안됨!");

        Debug.Log($"[Scale 적용] scale = {screenSize}");
    }
}