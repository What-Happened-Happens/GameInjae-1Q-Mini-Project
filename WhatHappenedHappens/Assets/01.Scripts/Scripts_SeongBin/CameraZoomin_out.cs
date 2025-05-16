using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

[ExecuteAlways]
public class PixelPerfectZoomCinemachine : MonoBehaviour
{
    [Range(1f, 20f)]
    public float targetZoom = 5f;               // 퍼블릭 변경 시에도 반영
    public bool isDiscrete = false;             // 즉시 변경 or 부드럽게
    public float zoomSpeed = 5f;                // 확대, 축소 속도
    public float screenMoveSpeed = 7f;          // 스크린이 움직이는 속도
    private float currentZoom;                  // 현재 화면의 크기
    private float previousZoom;                 // 퍼블릭 변경 감지를 위한 이전값 저장
    private float defaultScreenSize;              // 크기가 1일때 카메라의 y 값
    private int QClickCount;                      // q를 클릭한 횟수 -> 나중에 나누어 서 짝수면 q : off, 홀수면 q : on 
    private CinemachineVirtualCamera virtualCam;  // 버츄얼 카메라 컴포넌트 받을 변수
    private bool isScreenWide;                    // q 눌러서 스크린 키웠니? t/f     <- q 눌렀는지 안눌렀는지도 사용함 외부에서도 사용가능할것같아요?
    public Transform CameraLimit;                 // 전체 카메라의 움직임을 제한하는 오브젝트
   public Transform WideCameraPos;                // 카메라가 커졌을 때의 위치 
    public Transform NowPlayerPos;                // 현재 플레이어의 위치 

    // 할일 : 카메라 끝에가면 떨림....ㅜㅜ -> 벡터로 해결하면 될듯??
    //        카메라의 크기가 변하고 이동함에따라 화면의 경계 밖으로 살짝씩 이동 -> 해결법? 완전히 움직이고 줌아웃!!
    //        가끔씩 버그로 화면 밖을 벗어남 , y 축 1~2칸 정도?
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
        isScreenWide = false;
        virtualCam.Follow = NowPlayerPos;                      // 현제 카메라가 갈 곳을 플레이어로 정해놓음!!
        currentZoom = targetZoom;                            //현재 카메라의 크기
        previousZoom = targetZoom;                           // 과거 카메라의 크기
        ApplyZoomImmediate();
        QClickCount = 0;
        defaultScreenSize = Camera.main.orthographicSize * 2f / 5f;  // 기본 사이즈가 5f로 설정했음으로 /5, 카메라 수직길이 / 2임으로 *2를 해줌!!! 
        Debug.Log("디폴트 사이즈 :" + defaultScreenSize);
    }

    void Update()
    {
        DeciedWideMap();
        WideCameraLimit(); // 카메라 위치 이동!!!
        ApplyZoom();       // 확대 적용!!
    }

    private void LateUpdate()
    {
       
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

    // 외부 코드에서 호출 가능: 줌 변경 -> 외부에서 직접적으로 할려면 public 으로 바꾸기!!!
    void SetZoom(float newZoom, bool discrete)
    {
        targetZoom = Mathf.Clamp(newZoom, 1f, 20f);
        isDiscrete = discrete;

        if (isDiscrete)
        {
            ApplyZoomImmediate();
        }

        Debug.Log($"[외부 호출] targetZoom = {targetZoom}, isDiscrete = {isDiscrete}");
    }

    void ApplyZoom()
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

    private void ApplyZoomImmediate()
    {
        currentZoom = targetZoom;
        virtualCam.m_Lens.OrthographicSize = currentZoom;
    }


    void DeciedWideMap() // q의 클릭횟수가 양이면 F, 음이면T
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QClickCount++;
            //Debug.Log(QClickCount);
       }
        if (QClickCount % 2 == 0)
        {
            isScreenWide = false;
        }
        else
        {
            isScreenWide = true;
        }
    }


    // 카메라 경계 조건 수식
    // 카메라 가동범위X = 전체 카메라 경계의 크기 - 카메라 y 축 최대길이일때, x길이
    // 만약 :  전체 카메라의 중심 - (카메라 가동범위/2) > main 카메라의 중심점  =라면>  카메라가 갈수 있는 최대치로 돌리기
    // 범위 안에 있다면 방향키로 움직이기 가능!!!
    void WideCameraLimit()
    {
        if (isScreenWide == true)
        {
            float LimitMaxX = CameraLimit.localScale.x - CameraLimit.localScale.y * 16 / 9; //  가동범위
            if (CameraLimit.position.x - (LimitMaxX/2) > virtualCam.transform.position.x)
            {
                WideCameraPos.position = new Vector3(CameraLimit.position.x - (LimitMaxX / 2), CameraLimit.position.y, 0);
            }
            else if(CameraLimit.position.x + (LimitMaxX/2) < virtualCam.transform.position.x)
            {
                WideCameraPos.position = new Vector3(CameraLimit.position.x + (LimitMaxX / 2), CameraLimit.position.y, 0);
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    WideCameraPos.position += new Vector3(-1, 0, 0) * zoomSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    WideCameraPos.position += new Vector3(1, 0, 0) * zoomSpeed * Time.deltaTime;
                }
            }
            virtualCam.Follow = WideCameraPos;  // 버츄얼 카메라가 이 위치를 따라가게함...
           
            // 카메라 한계점과  카메라 size 값이 1일때의 길이를 나누어 size를 구하고 카메라 크기를 커지게하는 함수에 넣어줌!!
            SetZoom(CameraLimit.localScale.y / defaultScreenSize, false);  
            Debug.Log(CameraLimit.localScale.y / defaultScreenSize);
        }
        else
        {
            virtualCam.Follow = NowPlayerPos;
            SetZoom(5, false);
        }
    }




    // 카메라 전체화면 코드, 지금은 사용하지 않으나, 필요하면 해금가능!!!
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
    //        Debug.Log("비ㅣㅣㅣㅣ상!!!!   스크린 계산 오류!!");
    //    }
    //    Debug.Log(mulSize);
    //    return mulSize;
    //}
}