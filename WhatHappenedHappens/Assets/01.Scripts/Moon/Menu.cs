using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �� ESC ������ -> PAUSE �Ǹ鼭 �޴� UI ��
// �޴� UI���� ����� ���� ��ư Ŭ���ϸ� -> AudioSettingsUI �� 

// < �޴� UI >
// 1. �̾��ϱ� -> pause���� �� �޴� UI �ݱ�
// 2. ���� �ϱ� -> �� �ٽ� �ε��ϱ� (�̱��� �� �Ŵ��� )
// 3. ����� ���� -> AudioSettingsUI Ȱ��ȭ
// 4. �ɼ� -> AudioSettingsUI 
// 5. �κ�� ������ -> �� �Ŵ������� ���� ������ �̵�


public class Menu : MonoBehaviour
{
    public GameObject audioSettingsUI;

    void Start()
    {
        // 1. �̾��ϱ� 

        // 2. ���� �ϱ� 

        // 3. ����� ���� 

        // 4. �ɼ� 

        // 5. �κ�� ������ 

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) /*&& pauseScreen.GetComponent<pauseScreen>().isScreenPause*/)
        {
            audioSettingsUI.SetActive(false);
        }
    }
}
