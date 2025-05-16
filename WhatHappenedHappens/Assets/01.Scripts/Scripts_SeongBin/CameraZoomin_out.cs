using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

[ExecuteAlways]
public class PixelPerfectZoomCinemachine : MonoBehaviour
{
    [Range(1f, 10f)]
    public float targetZoom = 5f;               // �ۺ� ���� �ÿ��� �ݿ�
    public bool isDiscrete = false;             // ��� ���� or �ε巴��
    public float zoomSpeed = 5f;

    private float currentZoom;
    private float previousZoom;                 // �ۺ� ���� ������ ���� ������ ����
    private CinemachineVirtualCamera virtualCam;

    void Awake()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        if (virtualCam == null)
        {
            Debug.LogError("CinemachineVirtualCamera�� �����ϴ�.");
            enabled = false;
            return;
        }

        virtualCam.m_Lens.Orthographic = true;
    }

    void Start()
    {
        currentZoom = targetZoom;
        previousZoom = targetZoom;
        ApplyZoomImmediate();
    }

    void Update()
    {
        if (virtualCam == null) return;

        // �ۺ� ���� Inspector�� �ܺο��� ������� ��� ����
        if (!Mathf.Approximately(previousZoom, targetZoom))
        {
            previousZoom = targetZoom; // ���� ���� �� ����

            if (isDiscrete)
            {
                ApplyZoomImmediate();
            }
            // �ε巯�� ��ȯ�� ��� Update �������� �����
        }

        if (!isDiscrete && !Mathf.Approximately(currentZoom, targetZoom))
        {
            currentZoom = Mathf.MoveTowards(currentZoom, targetZoom, zoomSpeed * Time.deltaTime);
            virtualCam.m_Lens.OrthographicSize = currentZoom;
        }
    }

    //Inspector���� ���� ����� �� ȣ��Ǵ� �Լ�
    void OnValidate()
    {
        // �����Ϳ��� �� ���� �� ��� �ݿ�
        if (!Application.isPlaying && virtualCam == null)
        {
            virtualCam = GetComponent<CinemachineVirtualCamera>();
        }

        if (virtualCam != null)
        {
            virtualCam.m_Lens.Orthographic = true;
            virtualCam.m_Lens.OrthographicSize = targetZoom;
        }
    }

    // �ܺ� �ڵ忡�� ȣ�� ����: �� ����
    public void SetZoom(float newZoom, bool discrete)
    {
        targetZoom = Mathf.Clamp(newZoom, 1f, 10f);
        isDiscrete = discrete;

        if (isDiscrete)
        {
            ApplyZoomImmediate();
        }

        Debug.Log($"[�ܺ� ȣ��] targetZoom = {targetZoom}, isDiscrete = {isDiscrete}");
    }

    private void ApplyZoomImmediate()
    {
        currentZoom = targetZoom;
        virtualCam.m_Lens.OrthographicSize = currentZoom;
    }
}