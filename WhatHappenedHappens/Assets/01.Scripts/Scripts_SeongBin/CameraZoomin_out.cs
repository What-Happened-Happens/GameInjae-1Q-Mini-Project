using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.Rendering;

[ExecuteAlways]
public class PixelPerfectZoomCinemachine : MonoBehaviour
{
    [Range(1f, 20f)]
    public float defaultCameraSize;
    
    public float targetZoom  = 3f;               // �ۺ� ���� �ÿ��� �ݿ�
    public bool isDiscrete = false;             // ��� ���� or �ε巴��
    public float zoomSpeed = 5f;                // Ȯ��, ��� �ӵ�
    public float screenMoveSpeed = 7f;          // ��ũ���� �����̴� �ӵ�
    public float currentZoom;                  // ���� ȭ���� ũ��
    private float previousZoom;                 // �ۺ� ���� ������ ���� ������ ����
    private float defaultScreenSize;              // ũ�Ⱑ 1�϶� ī�޶��� y ��
    private CinemachineVirtualCamera virtualCam;  // ����� ī�޶� ������Ʈ ���� ����
    public bool isScreenWide = false;
    public pauseScreen pauseScreen;               // q�� ������ TF ���¸� ����
    public Transform CameraLimit;                 // ��ü ī�޶��� �������� �����ϴ� ������Ʈ
   public Transform WideCameraPos;                // ī�޶� Ŀ���� ���� ��ġ 
    public Transform NowPlayerPos;                // ���� �÷��̾��� ��ġ 
    public CinemachineBrain mainConvertUpdate;    // ����ī�޶��� ������Ʈ�ϴ°� �ٲٱ�
    float realDeltaTime = 0f;
    float lastTime = 0f;
    float delayTimeZoom = 0f;                          // �̵� -> ������ -> ��
                                                       //  ��  ->  ������ -> �̵�
    float delayTimeMove = 0f;
    // ���� : 
    //        ī�޶��� ũ�Ⱑ ���ϰ� �̵��Կ����� ȭ���� ��� ������ ��¦�� �̵� -> �ذ��? ������ �����̰� �ܾƿ�!!
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
        mainConvertUpdate.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
        virtualCam.Follow = NowPlayerPos;                      // ���� ī�޶� �� ���� �÷��̾�� ���س���!!
        currentZoom = targetZoom;                            //���� ī�޶��� ũ��
        previousZoom = targetZoom;                           // ���� ī�޶��� ũ��
        ApplyZoomImmediate();
        defaultScreenSize = Camera.main.orthographicSize * 2f / defaultCameraSize;  // �⺻ ����� 5f�� ������������ /5, ī�޶� �������� / 2������ *2�� ����!!! 
        // Debug.Log("����Ʈ ������ :" + defaultScreenSize);
    }

    void Update()
    {
        RealDeltaTime();
        WideCameraLimit();
        ApplyZoom();       // Ȯ�� ����!!
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

    void RealDeltaTime()
    {
        float currentTime = Time.realtimeSinceStartup;
        realDeltaTime = currentTime - lastTime;
        lastTime = currentTime;
        
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
        // ESC 1ȸ: ����
        if (pauseScreen.escPressCount % 2 == 1) return;
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
            if (pauseScreen.escPressCount % 2 == 1) return; // ���� ����
            currentZoom = Mathf.MoveTowards(currentZoom, targetZoom, zoomSpeed * realDeltaTime);
            virtualCam.m_Lens.OrthographicSize = currentZoom;
        }


    }

    private void ApplyZoomImmediate()
    {
        currentZoom = targetZoom;
        virtualCam.m_Lens.OrthographicSize = currentZoom;
    }


    


    // ī�޶� ��� ���� ����
    // ī�޶� ��������X = ��ü ī�޶� ����� ũ�� - ī�޶� y �� �ִ�����϶�, x����
    // ���� :  ��ü ī�޶��� �߽� - (ī�޶� ��������/2) > main ī�޶��� �߽���  =���>  ī�޶� ���� �ִ� �ִ�ġ�� ������
    // ���� �ȿ� �ִٸ� ����Ű�� �����̱� ����!!!
    public void WideCameraLimit()
    {
        if (pauseScreen.isScreenWide) { // q�� �ܺο��� ���������� 
            mainConvertUpdate.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
            float orthoSize = virtualCam.m_Lens.OrthographicSize;
            float cameraWidth = orthoSize * Camera.main.aspect * 2f;
            float halfWidth = cameraWidth / 2f;

            // ��ü ���� ������ ����
            float limitHalfWidth = CameraLimit.localScale.x / 2f;

            float camX = virtualCam.transform.position.x;
            float limitX = CameraLimit.position.x;

            if (camX - halfWidth + 0.05f < limitX - limitHalfWidth)
            {
                WideCameraPos.position = new Vector3(limitX - limitHalfWidth + halfWidth, CameraLimit.position.y, 0);
            }
            else if (camX + halfWidth - 0.05f > limitX + limitHalfWidth)
            {
                WideCameraPos.position = new Vector3(limitX + limitHalfWidth - halfWidth, CameraLimit.position.y, 0);
            }
            else
            {
                WideCameraPos.position = new Vector3(WideCameraPos.position.x, CameraLimit.position.y, 0);
                //Debug.Log(CameraLimit.position.y);
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    //Debug.Log(realDeltaTime);
                    WideCameraPos.position += new Vector3(-1, 0, 0) * zoomSpeed * realDeltaTime;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    WideCameraPos.position += new Vector3(1, 0, 0) * zoomSpeed * realDeltaTime;
                }
            }
            virtualCam.Follow = WideCameraPos;  // ����� ī�޶� �� ��ġ�� ���󰡰���...

            //���� �� 
            // ī�޶� �Ѱ�����  ī�޶� size ���� 1�϶��� ���̸� ������ size�� ���ϰ� ī�޶� ũ�⸦ Ŀ�����ϴ� �Լ��� �־���!!
                SetZoom(CameraLimit.localScale.y / defaultScreenSize, false);

            //Debug.Log(CameraLimit.localScale.y / defaultScreenSize);
        }
        else
        {
            mainConvertUpdate.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
            SetZoom(defaultCameraSize, false);
            //���� ���� �������
                virtualCam.Follow = NowPlayerPos;
           
        }
    }

    void calTime()
    {
        if (!pauseScreen.isScreenPause)
        {
            if (pauseScreen.isScreenWide)
            {
                delayTimeMove += realDeltaTime;
                delayTimeZoom = 0;
            }
            else {
                delayTimeZoom += realDeltaTime;
                delayTimeMove = 0;
            }
        }
        
    }
}