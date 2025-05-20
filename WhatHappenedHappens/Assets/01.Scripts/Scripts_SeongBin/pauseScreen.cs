using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScreen : MonoBehaviour
{
    // 멈춤 기능 사용하기!!
    // 1. hierachy 창에 빈 GameObject하나 생성 후, 해당 스크립트를 컴포넌트로 넣기
    // 2. virtual camera에 있는 스크립트에 pause Screen에 해당 스크립트가 담긴 빈 오브젝트 넣기
    // 3. vritual camera에 Ignore Timne Sclae 항목 체크 !!
    // 3. main Camera에 있는 Update Method fixed Update가 아닌 다른 업데이트로 바꾸기
    //    => fixed Update는 TimeScale에 영향을 받음
    //끗
    public bool isScreenWide;
    // Start is called before the first frame update
    
    void Update()
    {
        ToggleWideScreen();
        TimeStop();
    }
    void ToggleWideScreen()  // q를 눌렀을때, bool값 변경
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isScreenWide = !isScreenWide;
            Debug.Log("Q 눌림: isScreenWide = " + isScreenWide);
        }
    }
    
    void TimeStop() // TimeScale을 조정해서 Time을 사용하는 대부분의 물체를 멈춤
    {
        if (isScreenWide)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}

