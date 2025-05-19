using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScreen : MonoBehaviour
{
    // ���� ��� ����ϱ�!!
    // 1. hierachy â�� �� GameObject�ϳ� ���� ��, �ش� ��ũ��Ʈ�� ������Ʈ�� �ֱ�
    // 2. virtual camera�� �ִ� ��ũ��Ʈ�� pause Screen�� �ش� ��ũ��Ʈ�� ��� �� ������Ʈ �ֱ�
    // 3. vritual camera�� Ignore Timne Sclae �׸� üũ !!
    // 3. main Camera�� �ִ� Update Method fixed Update�� �ƴ� �ٸ� ������Ʈ�� �ٲٱ�
    //    => fixed Update�� TimeScale�� ������ ����
    //��
    public bool isScreenWide;
    // Start is called before the first frame update
    
    void Update()
    {
        ToggleWideScreen();
        TimeStop();
    }
    void ToggleWideScreen()  // q�� ��������, bool�� ����
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isScreenWide = !isScreenWide;
            Debug.Log("Q ����: isScreenWide = " + isScreenWide);
        }
    }
    
    void TimeStop() // TimeScale�� �����ؼ� Time�� ����ϴ� ��κ��� ��ü�� ����
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

