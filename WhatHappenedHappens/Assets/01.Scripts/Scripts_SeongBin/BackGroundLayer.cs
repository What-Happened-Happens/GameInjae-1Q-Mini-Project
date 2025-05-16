using UnityEngine;

public class BackGroundLayer : MonoBehaviour
{
    // 패럴럭스 효과를 줄 모든 배경 오브젝트들 (앞, 뒤 배경 포함)
    public Transform[] backgrounds;

    // 패럴럭스 효과의 부드러움 정도 (값이 클수록 부드럽게 움직임)
    public float smoothing = 1f;

    // 각 배경이 카메라 이동에 얼마나 반응할지 결정하는 비율 (Z값 기반)
    private float[] parallaxScales;

    // 메인 카메라의 Transform 참조
    private Transform cam;

    // 이전 프레임의 카메라 위치
    private Vector3 previousCamPos;    

    //배경의 상대 움직임 정도를 조절할 수 있는 수!!
    public float bGRelativePos;

    // Start()보다 먼저 호출되며, 참조 설정에 적합한 시점
    void Awake()
    {
        // 메인 카메라의 Transform 가져오기
        this.cam = Camera.main.transform;
    }

    // 초기 설정 (게임 시작 시 호출됨)
    void Start()
    {
        bGRelativePos = 20f;
        // 이전 프레임의 카메라 위치를 현재 위치로 초기화
        this.previousCamPos = this.cam.position;   

        // 배경 수만큼 parallaxScales 배열 초기화
        this.parallaxScales = new float[this.backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (this.backgrounds[i].position.z > 0)
            {
                // Z값에 따라 움직임 비율 설정 (멀수록 느리게 움직이도록)
                this.parallaxScales[i] = backgrounds[i].position.z * -bGRelativePos;
            }
            else if(this.backgrounds[i].position.z < 0)  // 이거는 실험 필요!!
            {
                this.parallaxScales[i] = backgrounds[i].position.z * -bGRelativePos * (-1f);
            }
            
        }
    }

    // 매 프레임마다 실행됨
    void Update()
    {
        for (int i = 0; i < this.backgrounds.Length; i++)
        {
            // 이전 프레임과 현재 프레임 사이 카메라의 이동량 * 배경 비율 계산
            Vector2 parallax = (this.previousCamPos - this.cam.position) * this.parallaxScales[i];

            // 배경의 목표 위치 계산 (X, Y 각각 적용)
            float backgroundTargetPosX = this.backgrounds[i].position.x + parallax.x;
            float backgroundTargetPosY = this.backgrounds[i].position.y + parallax.y;

            // 최종 이동 위치 설정 (Z는 그대로 유지)
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, this.backgrounds[i].position.z);

            // 현재 위치와 목표 위치 사이를 부드럽게 이동 (Lerp 사용)
            this.backgrounds[i].position = Vector3.Lerp(this.backgrounds[i].position, backgroundTargetPos, this.smoothing * Time.deltaTime);
        }

        // 다음 프레임을 위해 현재 카메라 위치 저장
        this.previousCamPos = this.cam.position;
    }
}
