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
        if (!virtualCam) Debug.LogError("Virtual Camera ���� �ȵ�!");
        screenSize = 5;
    }

    void Update()
    {
        ApplyZoom();
    }

    public void SetScreenSize(float newZoom) // ��ũ���� ũ�� �ٷ� ����
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
            Debug.Log("����� �� ����!!"); 
            return; 
        }

        virtualCam.m_Lens.OrthographicSize = screenSize;
        if (!virtualCam) Debug.LogError("Virtual Camera ���� �ȵ�!");

        Debug.Log($"[Scale ����] scale = {screenSize}");
    }
}