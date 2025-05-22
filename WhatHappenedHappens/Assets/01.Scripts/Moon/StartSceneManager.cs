using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public GameObject AudioSettingUI;

    private void Start()
    {
        // ù��° Ŭ�� ��� 
        SoundManager.Instance.PlayBGM(SoundManager.Instance.bgmClips[0]);
    }

    public void StartGame()
    {
        SoundManager.Instance.UISFX("UI_Click", 1f);
        // ���� ���� ����
        SceneController.Instance.LoadScene("Stage1Scene");
    }

    public void LoadOptions()
    {
        SoundManager.Instance.UISFX("UI_Click", 1f);

        // AudioSettingUI Ȱ��ȭ 
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

        // ���� ���� ����
        Application.Quit();
        Debug.Log("���� ����");
    }

}
