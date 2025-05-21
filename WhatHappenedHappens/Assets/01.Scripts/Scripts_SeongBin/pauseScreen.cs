using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScreen : MonoBehaviour
{
    // 카메라 멈춤 기능 사용하기!!
    // 1. hierachy 창에 빈 GameObject하나 생성 후, 해당 스크립트를 컴포넌트로 넣기
    // 2. virtual camera에 있는 스크립트에 pause Screen에 해당 스크립트가 담긴 빈 오브젝트 넣기
    // 3. vritual camera에 Ignore Timne Sclae 항목 체크 !!
    // 3. main Camera에 있는 Update Method fixed Update가 아닌 다른 업데이트로 바꾸기
    //    => fixed Update는 TimeScale에 영향을 받음
    //끗

    // esc 멈춤기능 사용하기
    // 
    public bool isScreenWide;   //화면 키울래?, t/f
    public bool isScreenPause;  //ESC눌렀니? , t/f
                                //-> ESC 누르고 나오는 UI 넣을 때 사용하면 좋을것같음! 
    bool storeScreenPause;      //ESC 눌렀을 때, Q 상태를 저장할 코드
    bool applyStoredScreenWide = false;
    // Start is called before the first frame update

    void Update()
    {
        ToggleStopScreen();
        TimeStop();
    }
    void ToggleStopScreen()  // q, esc를 눌렀을때, bool값 변경
    {
        // Q키로 화면 정지 상태 토글 (ESC 눌린 상태가 아니어야만 반응)
        if (!isScreenPause && Input.GetKeyDown(KeyCode.Q))
        {
            isScreenWide = !isScreenWide;
            //Debug.Log("Q 눌림: isScreenWide = " + isScreenWide);
        }

        // ESC 키 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isScreenPause = !isScreenPause;

            if (isScreenPause)
            {
                // 화면이 멈출 때 Q 상태 저장 + Q 해제
                storeScreenPause = isScreenWide;
                isScreenWide = false;
            }
            else
            {
                // 화면이 다시 돌아올 때 저장값을 다음 프레임에서 적용
                applyStoredScreenWide = true;
            }

            Debug.Log("ESC 눌림: isScreenPause = " + isScreenPause);
        }

        //다음 프레임에서 Q 상태 복구
        if (applyStoredScreenWide)
        {
            isScreenWide = storeScreenPause;
           // Debug.Log("Q 복원: isScreenWide = " + isScreenWide);
            storeScreenPause = false;
            applyStoredScreenWide = false;
        }

    }
    
    void TimeStop() // TimeScale을 조정해서 Time을 사용하는 대부분의 물체를 멈춤
    {
        if (isScreenWide || isScreenPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}

