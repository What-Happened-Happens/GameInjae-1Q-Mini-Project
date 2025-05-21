using UnityEngine;

public class TemporaryPausePopUp : MonoBehaviour
{
    // �ӽ÷� ���� �ʱ�ȭ 
    private bool isPopUpActived; 
    private bool isEscPressed;

    public Canvas _targetCanvas; 

    private void Start()
    {
        _targetCanvas =  GetComponent<Canvas>();

        _targetCanvas.gameObject.SetActive(true);
        Debug.Log($"�׽�Ʈ�� ���ؼ� �Ͻ������� Ȱ��ȭ ���·� ����. TemporaryPausePopUp ");
        isPopUpActived = true;
        Debug.Log($"�׽�Ʈ�� ���ؼ� �Ͻ������� Ȱ��ȭ ���·� ����. TemporaryPausePopUp isPopUpActived : {isPopUpActived} ");
        isEscPressed = false;   
                
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // ESC Ű�� ������ �� 
        {
            isEscPressed = true;
        }
      
        TemporaryPausePopUpControl(isEscPressed);
    }

    // ESC Ű �Է� ���¿� ���� �˾� â Show 
    public void TemporaryPausePopUpControl (bool isEscPressed)
    {
        
        if (isEscPressed && !isPopUpActived)  // EscŰ�� ������ ��, �˾� â�� Ȱ��ȭ ���°� �ƴ� �� 
        {
            Debug.Log($"ESC Ű�� �������ϴ�! > {isEscPressed}");
            Debug.Log($"���� �˾� ����  > {isPopUpActived}");
            _targetCanvas.gameObject.SetActive(true);
            isPopUpActived = true; 
        }
        else if (isEscPressed && isPopUpActived) // EscŰ�� ������ ��, �˾� â�� Ȱ��ȭ ������ �� 
        {
            Debug.Log($"ESC Ű�� �������ϴ�! > {isEscPressed}");
            Debug.Log($"���� �˾� ����  > {isPopUpActived}");
            _targetCanvas.gameObject.SetActive(false);
            isPopUpActived = false;
        }
        
    
    }

}
