using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Stage1Manager : MonoBehaviour
{
    public GameObject AudioSettingUI; 
    public GameObject MenuUI;

    public GameObject pauseScreen;
    public GameObject FinishGate;

    public GameObject Player;

    private bool isFinish = false;

    private void Start()
    {
        // �ι�° Ŭ�� ���
        SoundManager.Instance.PlayBGM(SoundManager.Instance.bgmClips[1]);
    }

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

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneController.Instance.LoadScene("Stage2Scene");
        }

        if (Player.GetComponent<Player>().isDead)
        {
            StartCoroutine(WaitAndLoadStartScene());
        }
    }

    IEnumerator WaitAndLoadNextScene()
    {
        yield return new WaitForSeconds(4f);
        // ���� �� �ε�
        SceneController.Instance.LoadScene("Stage2Scene");
    }

    IEnumerator WaitAndLoadStartScene()
    {
        yield return new WaitForSeconds(2f);
        SceneController.Instance.LoadScene("StartScene");
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
