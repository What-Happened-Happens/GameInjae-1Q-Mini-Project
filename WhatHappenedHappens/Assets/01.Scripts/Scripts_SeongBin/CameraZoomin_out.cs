using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.Rendering;

[ExecuteAlways]
public class PixelPerfectZoomCinemachine : MonoBehaviour
{
    [Range(1f, 20f)]
    public float defaultCameraSize;
    
    public float targetZoom  = 3f;               // 퍼블릭 변경 시에도 반영
    public bool isDiscrete = false;             // 즉시 변경 or 부드럽게
    public float zoomSpeed = 5f;                // 확대, 축소 속도
    public float screenMoveSpeed = 7f;          // 스크린이 움직이는 속도
    public float currentZoom;                  // 현재 화면의 크기
    private float previousZoom;                 // 퍼블릭 변경 감지를 위한 이전값 저장
    private float defaultScreenSize;              // 크기가 1일때 카메라의 y 값
    private CinemachineVirtualCamera virtualCam;  // 버츄얼 카메라 컴포넌트 받을 변수
    public bool isScreenWide = false;
    public pauseScreen pauseScreen;               // q를 누르면 TF 상태를 만듦
    public Transform CameraLimit;                 // 전체 카메라의 움직임을 제한하는 오브젝트
   public Transform WideCameraPos;                // 카메라가 커졌을 때의 위치 
    public Transform NowPlayerPos;                // 현재 플레이어의 위치 
    public CinemachineBrain mainConvertUpdate;    // 메인카메라의 업데이트하는곳 바꾸기
    float realDeltaTime = 0f;
    float lastTime = 0f;
    float delayTimeZoom = 0f;                          // 이동 -> 딜레이 -> 줌
                                                       //  줌  ->  딜레이 -> 이동
    float delayTimeMove = 0f;
    // 할일 : 
    //        카메라의 크기가 변하고 이동함에따라 화면의 경계 밖으로 살짝씩 이동 -> 해결법? 완전히 움직이고 줌아웃!!
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
        mainConvertUpdate.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
        virtualCam.Follow = NowPlayerPos;                      // 현제 카메라가 갈 곳을 플레이어로 정해놓음!!
        currentZoom = targetZoom;                            //현재 카메라의 크기
        previousZoom = targetZoom;                           // 과거 카메라의 크기
        ApplyZoomImmediate();
        defaultScreenSize = Camera.main.orthographicSize * 2f / defaultCameraSize;  // 기본 사이즈가 5f로 설정했음으로 /5, 카메라 수직길이 / 2임으로 *2를 해줌!!! 
        // Debug.Log("디폴트 사이즈 :" + defaultScreenSize);
    }

    void Update()
    {
        RealDeltaTime();
        WideCameraLimit();
        ApplyZoom();       // 확대 적용!!
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

    void RealDeltaTime()
    {
        float currentTime = Time.realtimeSinceStartup;
        realDeltaTime = currentTime - lastTime;
        lastTime = currentTime;
        
    }


    // 외부 코드에서 호출 가능: 줌 변경 -> 외부에서 직접적으로 할려면 public 으로 바꾸기!!!
    void SetZoom(float newZoom, bool discrete)
    {
        targetZoom = Mathf.Clamp(newZoom, 1f, 20f);
        isDiscrete = discrete;

        if (isDiscrete)
        {
            ApplyZoomImmediate();
        }

        // Debug.Log($"[외부 호출] targetZoom = {targetZoom}, isDiscrete = {isDiscrete}");
    }

    void ApplyZoom()
    {
        if (virtualCam == null) return;
        // ESC 1회: 정지
        if (pauseScreen.escPressCount % 2 == 1) return;
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
            if (pauseScreen.escPressCount % 2 == 1) return; // 정지 상태
            currentZoom = Mathf.MoveTowards(currentZoom, targetZoom, zoomSpeed * realDeltaTime);
            virtualCam.m_Lens.OrthographicSize = currentZoom;
        }


    }

    private void ApplyZoomImmediate()
    {
        currentZoom = targetZoom;
        virtualCam.m_Lens.OrthographicSize = currentZoom;
    }


    


    // 카메라 경계 조건 수식
    // 카메라 가동범위X = 전체 카메라 경계의 크기 - 카메라 y 축 최대길이일때, x길이
    // 만약 :  전체 카메라의 중심 - (카메라 가동범위/2) > main 카메라의 중심점  =라면>  카메라가 갈수 있는 최대치로 돌리기
    // 범위 안에 있다면 방향키로 움직이기 가능!!!
    public void WideCameraLimit()
    {
        if (pauseScreen.isScreenWide) { // q가 외부에서 눌러졌을때 
            mainConvertUpdate.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
            float orthoSize = virtualCam.m_Lens.OrthographicSize;
            float cameraWidth = orthoSize * Camera.main.aspect * 2f;
            float halfWidth = cameraWidth / 2f;

            // 전체 제한 영역의 절반
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
            virtualCam.Follow = WideCameraPos;  // 버츄얼 카메라가 이 위치를 따라가게함...

            //거의 다 
            // 카메라 한계점과  카메라 size 값이 1일때의 길이를 나누어 size를 구하고 카메라 크기를 커지게하는 함수에 넣어줌!!
                SetZoom(CameraLimit.localScale.y / defaultScreenSize, false);

            //Debug.Log(CameraLimit.localScale.y / defaultScreenSize);
        }
        else
        {
            mainConvertUpdate.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
            SetZoom(defaultCameraSize, false);
            //줌이 거의 끝날경우
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