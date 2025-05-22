using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public GameObject AudioSettingUI;

    private void Start()
    {
        // 첫번째 클립 재생 
        SoundManager.Instance.PlayBGM(SoundManager.Instance.bgmClips[0]);
    }

    public void StartGame()
    {
        SoundManager.Instance.UISFX("UI_Click", 1f);
        // 게임 시작 로직
        SceneController.Instance.LoadScene("Stage1Scene");
    }

    public void LoadOptions()
    {
        SoundManager.Instance.UISFX("UI_Click", 1f);

        // AudioSettingUI 활성화 
        if (AudioSettingUI.activeSelf)
        {
            AudioSettingUI.SetActive(false);
        }
        else
        {
            AudioSettingUI.SetActive(true);
        }
    }

    public void QuitGame()
    {
        SoundManager.Instance.UISFX("UI_Click", 1f);

        // 게임 종료 로직
        Application.Quit();
        Debug.Log("게임 종료");
    }

}
