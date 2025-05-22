using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScreen : MonoBehaviour
{
    // ī�޶� ���� ��� ����ϱ�!!
    // 1. hierachy â�� �� GameObject�ϳ� ���� ��, �ش� ��ũ��Ʈ�� ������Ʈ�� �ֱ�
    // 2. virtual camera�� �ִ� ��ũ��Ʈ�� pause Screen�� �ش� ��ũ��Ʈ�� ��� �� ������Ʈ �ֱ�
    // 3. vritual camera�� Ignore Timne Sclae �׸� üũ !!
    // 3. main Camera�� �ִ� Update Method fixed Update�� �ƴ� �ٸ� ������Ʈ�� �ٲٱ�
    //    => fixed Update�� TimeScale�� ������ ����
    //��

    // esc ������ ����ϱ�
    // 
    public bool isScreenWide;   //ȭ�� Ű�﷡?, t/f
    public bool isScreenPause;  //ESC������? , t/f
                                //-> ESC ������ ������ UI ���� �� ����ϸ� �����Ͱ���! 
    bool storeScreenPause;      //ESC ������ ��, Q ���¸� ������ �ڵ�
    bool applyStoredScreenWide = false;
    // Start is called before the first frame update

    void Update()
    {
        ToggleStopScreen();
        TimeStop();
    }
    void ToggleStopScreen()  // q, esc�� ��������, bool�� ����
    {
        // QŰ�� ȭ�� ���� ���� ��� (ESC ���� ���°� �ƴϾ�߸� ����)
        if (!isScreenPause && Input.GetKeyDown(KeyCode.Q))
        {
            isScreenWide = !isScreenWide;
            //Debug.Log("Q ����: isScreenWide = " + isScreenWide);
        }

        // ESC Ű ������ ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isScreenPause = !isScreenPause;

            if (isScreenPause)
            {
                // ȭ���� ���� �� Q ���� ���� + Q ����
                storeScreenPause = isScreenWide;
                isScreenWide = false;
            }
            else
            {
                // ȭ���� �ٽ� ���ƿ� �� ���尪�� ���� �����ӿ��� ����
                applyStoredScreenWide = true;
            }

            Debug.Log("ESC ����: isScreenPause = " + isScreenPause);
        }

        //���� �����ӿ��� Q ���� ����
        if (applyStoredScreenWide)
        {
            isScreenWide = storeScreenPause;
           // Debug.Log("Q ����: isScreenWide = " + isScreenWide);
            storeScreenPause = false;
            applyStoredScreenWide = false;
        }

    }
    
    void TimeStop() // TimeScale�� �����ؼ� Time�� ����ϴ� ��κ��� ��ü�� ����
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

