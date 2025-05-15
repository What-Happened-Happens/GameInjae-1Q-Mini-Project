using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

[ExecuteAlways]
public class PixelPerfectZoomCinemachine : MonoBehaviour
{
    [Range(1f, 10f)]
    public float targetZoom = 5f;               // 퍼블릭 변경 시에도 반영
    public bool isDiscrete = false;             // 즉시 변경 or 부드럽게
    public float zoomSpeed = 5f;

    private float currentZoom;
    private float previousZoom;                 // 퍼블릭 변경 감지를 위한 이전값 저장
    private CinemachineVirtualCamera virtualCam;

    void Awake()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        if (virtualCam == null)
        {
            Debug.LogError("CinemachineVirtualCamera가 없습니다.");
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

        // 퍼블릭 값이 Inspector나 외부에서 변경됐을 경우 감지
        if (!Mathf.Approximately(previousZoom, targetZoom))
        {
            previousZoom = targetZoom; // 변경 감지 후 저장

            if (isDiscrete)
            {
                ApplyZoomImmediate();
            }
            // 부드러운 전환은 계속 Update 루프에서 실행됨
        }

        if (!isDiscrete && !Mathf.Approximately(currentZoom, targetZoom))
        {
            currentZoom = Mathf.MoveTowards(currentZoom, targetZoom, zoomSpeed * Time.deltaTime);
            virtualCam.m_Lens.OrthographicSize = currentZoom;
        }
    }

    //Inspector에서 값이 변경될 때 호출되는 함수
    void OnValidate()
    {
        // 에디터에서 값 변경 시 즉시 반영
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

    // 외부 코드에서 호출 가능: 줌 변경
    public void SetZoom(float newZoom, bool discrete)
    {
        targetZoom = Mathf.Clamp(newZoom, 1f, 10f);
        isDiscrete = discrete;

        if (isDiscrete)
        {
            ApplyZoomImmediate();
        }

        Debug.Log($"[외부 호출] targetZoom = {targetZoom}, isDiscrete = {isDiscrete}");
    }

    private void ApplyZoomImmediate()
    {
        currentZoom = targetZoom;
        virtualCam.m_Lens.OrthographicSize = currentZoom;
    }
}