using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Manager : MonoBehaviour
{
    public GameObject AudioSettingUI;
    public GameObject MenuUI;

    public GameObject pauseScreen;
    public GameObject FinishGate;

    private bool isFinish = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !MenuUI.activeSelf)
        {
            MenuUI.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && MenuUI.activeSelf)
        {
            AudioSettingUI.SetActive(false);
            MenuUI.SetActive(false);
        }

        if (FinishGate.GetComponent<FinishGate>().isOpened && !isFinish)
        {
            isFinish = true;
            StartCoroutine(WaitAndLoadNextScene());
        }
    }

    IEnumerator WaitAndLoadNextScene()
    {
        yield return new WaitForSeconds(4f);
        // ���� �� �ε�
        SceneController.Instance.LoadScene("Stage4Scene");
    }

    public void ContinuePlay()
    {
        SoundManager.Instance.UISFX("UI_Click", 1f);

        AudioSettingUI.SetActive(false);
        MenuUI.SetActive(false);

        // pause �����ϱ� 
        pauseScreen.GetComponent<pauseScreen>().isScreenPause = false;
    }

    public void NewGame()
    {
        SoundManager.Instance.UISFX("UI_Click", 1f);
        // ���� �� ���� ���� ����
        SceneController.Instance.ReloadCurrentScene();
    }

    public void LoadOptions()
    {
        SoundManager.Instance.UISFX("UI_Click", 1f);

        if (AudioSettingUI.activeSelf)
        {
            AudioSettingUI.SetActive(false);
        }
        else
        {
            AudioSettingUI.SetActive(true);
        }
    }

    public void GotoStart()
    {
        SoundManager.Instance.UISFX("UI_Click", 1f);
        SceneController.Instance.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        SoundManager.Instance.UISFX("UI_Click", 1f);

        // ���� ���� ����
        Application.Quit();
        Debug.Log("���� ����");
    }
}
