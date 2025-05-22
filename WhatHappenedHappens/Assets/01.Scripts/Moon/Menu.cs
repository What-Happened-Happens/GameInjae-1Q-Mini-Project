using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 중 ESC 누르면 -> PAUSE 되면서 메뉴 UI 뜲
// 메뉴 UI에서 오디오 설정 버튼 클릭하면 -> AudioSettingsUI 뜲 

// < 메뉴 UI >
// 1. 이어하기 -> pause해제 및 메뉴 UI 닫기
// 2. 새로 하기 -> 씬 다시 로드하기 (싱글톤 씬 매니저 )
// 3. 오디오 설정 -> AudioSettingsUI 활성화
// 4. 옵션 -> AudioSettingsUI 
// 5. 로비로 나가기 -> 씬 매니저에서 시작 씬으로 이동


public class Menu : MonoBehaviour
{
    public GameObject audioSettingsUI;

    void Start()
    {
        // 1. 이어하기 

        // 2. 새로 하기 

        // 3. 오디오 설정 

        // 4. 옵션 

        // 5. 로비로 나가기 

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) /*&& pauseScreen.GetComponent<pauseScreen>().isScreenPause*/)
        {
            audioSettingsUI.SetActive(false);
        }
    }
}
