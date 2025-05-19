using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

[ExecuteAlways]
public class PixelPerfectZoomCinemachine : MonoBehaviour
{
    [Range(1f, 20f)]
    public float targetZoom = 5f;               // �ۺ� ���� �ÿ��� �ݿ�
    public bool isDiscrete = false;             // ��� ���� or �ε巴��
    public float zoomSpeed = 5f;                // Ȯ��, ��� �ӵ�
    public float screenMoveSpeed = 7f;          // ��ũ���� �����̴� �ӵ�
    private float currentZoom;                  // ���� ȭ���� ũ��
    private float previousZoom;                 // �ۺ� ���� ������ ���� ������ ����
    private float defaultScreenSize;              // ũ�Ⱑ 1�϶� ī�޶��� y ��
    private CinemachineVirtualCamera virtualCam;  // ����� ī�޶� ������Ʈ ���� ����
    public bool isScreenWide = false;
    int qCount;
    public Transform CameraLimit;                 // ��ü ī�޶��� �������� �����ϴ� ������Ʈ
   public Transform WideCameraPos;                // ī�޶� Ŀ���� ���� ��ġ 
    public Transform NowPlayerPos;                // ���� �÷��̾��� ��ġ 

    // ���� : ī�޶� �������� ����....�̤� -> ���ͷ� �ذ��ϸ� �ɵ�??
    //        ī�޶��� ũ�Ⱑ ���ϰ� �̵��Կ����� ȭ���� ��� ������ ��¦�� �̵� -> �ذ��? ������ �����̰� �ܾƿ�!!
    //        ������ ���׷� ȭ�� ���� ��� , y �� 1~2ĭ ����?
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
        qCount = 0;
        virtualCam.Follow = NowPlayerPos;                      // ���� ī�޶� �� ���� �÷��̾�� ���س���!!
        currentZoom = targetZoom;                            //���� ī�޶��� ũ��
        previousZoom = targetZoom;                           // ���� ī�޶��� ũ��
        ApplyZoomImmediate();
        
        defaultScreenSize = Camera.main.orthographicSize * 2f / 5f;  // �⺻ ����� 5f�� ������������ /5, ī�޶� �������� / 2������ *2�� ����!!! 
        // Debug.Log("����Ʈ ������ :" + defaultScreenSize);
    }

    void Update()
    {
        ToggleWideScreen();
        WideCameraLimit();
        ApplyZoom();       // Ȯ�� ����!!
    }

    private void LateUpdate()
    {
       
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

    // �ܺ� �ڵ忡�� ȣ�� ����: �� ���� -> �ܺο��� ���������� �ҷ��� public ���� �ٲٱ�!!!
    void SetZoom(float newZoom, bool discrete)
    {
        targetZoom = Mathf.Clamp(newZoom, 1f, 20f);
        isDiscrete = discrete;

        if (isDiscrete)
        {
            ApplyZoomImmediate();
        }

        // Debug.Log($"[�ܺ� ȣ��] targetZoom = {targetZoom}, isDiscrete = {isDiscrete}");
    }

    void ApplyZoom()
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
            currentZoom = Mathf.MoveTowards(currentZoom, targetZoom, zoomSpeed * Time.unscaledDeltaTime);
            virtualCam.m_Lens.OrthographicSize = currentZoom;
        }
    }

    private void ApplyZoomImmediate()
    {
        currentZoom = targetZoom;
        virtualCam.m_Lens.OrthographicSize = currentZoom;
    }


    void ToggleWideScreen()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isScreenWide = !isScreenWide;
            Debug.Log("Q ����: isScreenWide = " + isScreenWide);
        }
    }


    // ī�޶� ��� ���� ����
    // ī�޶� ��������X = ��ü ī�޶� ����� ũ�� - ī�޶� y �� �ִ�����϶�, x����
    // ���� :  ��ü ī�޶��� �߽� - (ī�޶� ��������/2) > main ī�޶��� �߽���  =���>  ī�޶� ���� �ִ� �ִ�ġ�� ������
    // ���� �ȿ� �ִٸ� ����Ű�� �����̱� ����!!!
    public void WideCameraLimit()
    {
        if (isScreenWide) {
            float screenWidth = CameraLimit.localScale.y * Camera.main.aspect;
            float LimitMaxX = CameraLimit.localScale.x - screenWidth;

            if (CameraLimit.position.x - (LimitMaxX / 2) > virtualCam.transform.position.x)
            {
                WideCameraPos.position = new Vector3(CameraLimit.position.x - (LimitMaxX / 2), CameraLimit.position.y, 0);
            }
            else if (CameraLimit.position.x + (LimitMaxX / 2) < virtualCam.transform.position.x)
            {
                WideCameraPos.position = new Vector3(CameraLimit.position.x + (LimitMaxX / 2), CameraLimit.position.y, 0);
            }
            else
            {
                WideCameraPos.position = new Vector3(WideCameraPos.position.x, CameraLimit.position.y, 0);
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    WideCameraPos.position += new Vector3(-1, 0, 0) * zoomSpeed * Time.unscaledDeltaTime;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    WideCameraPos.position += new Vector3(1, 0, 0) * zoomSpeed * Time.unscaledDeltaTime;
                }
            }
            virtualCam.Follow = WideCameraPos;  // ����� ī�޶� �� ��ġ�� ���󰡰���...

            // ī�޶� �Ѱ�����  ī�޶� size ���� 1�϶��� ���̸� ������ size�� ���ϰ� ī�޶� ũ�⸦ Ŀ�����ϴ� �Լ��� �־���!!
            SetZoom(CameraLimit.localScale.y / defaultScreenSize, false);
            Debug.Log(CameraLimit.localScale.y / defaultScreenSize);
        }
        else
        {
            virtualCam.Follow = NowPlayerPos;
            SetZoom(5, false);
        }
    }





    // ī�޶� ��üȭ�� �ڵ�, ������ ������� ������, �ʿ��ϸ� �رݰ���!!!
    //float CalScreen()
    //{
    //    float mulSize;
    //    if (CameraLimit.localScale.y >= CameraLimit.localScale.x * Camera.main.aspect)
    //    {
    //        mulSize = CameraLimit.localScale.y / defaultScreenSize;

    //    }

    //    else if (CameraLimit.localScale.y < CameraLimit.localScale.x * Camera.main.aspect)
    //    {
    //        mulSize = CameraLimit.localScale.x * Camera.main.aspect / defaultScreenSize;
    //    }
    //    else
    //    {
    //        mulSize = 0f;
    //        Debug.Log("��ӤӤӤӻ�!!!!   ��ũ�� ��� ����!!");
    //    }
    //    Debug.Log(mulSize);
    //    return mulSize;
    //}
}