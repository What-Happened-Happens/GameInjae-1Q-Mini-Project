using UnityEngine;

public class TemporaryPausePopUp : MonoBehaviour
{
    // 임시로 지정 초기화 
    private bool isPopUpActived; 
    private bool isEscPressed;

    public Canvas _targetCanvas; 

    private void Start()
    {
        _targetCanvas =  GetComponent<Canvas>(); 

        Debug.Log($"테스트를 위해서 일시적으로 활성화 상태로 지정. TemporaryPausePopUp ");
        gameObject.SetActive(true);
        Debug.Log($"테스트를 위해서 일시적으로 활성화 상태로 지정. TemporaryPausePopUp : {isPopUpActived} ");
        isPopUpActived = true;
        Debug.Log($"테스트를 위해서 일시적으로 비활성화 상태로 지정. TemporaryPausePopUp : {isEscPressed} ");
        isEscPressed = false; 
    }
   
    // ESC 키 입력 상태에 따라서 팝업 창 Show 
    public void TemporaryPausePopUpControl (bool isEscPressed)
    {
        if (isEscPressed == false ) return;

        if (isEscPressed && !isPopUpActived)  // Esc키를 눌렀을 때, 팝업 창이 활성화 상태가 아닐 때 
        {
            Debug.Log($"ESC 키를 눌렀습니다! > {isEscPressed}");
            Debug.Log($"현재 팝업 상태  > {isPopUpActived}");
            _targetCanvas.gameObject.SetActive(true);
        }
        else if (isEscPressed && isPopUpActived) // Esc키를 눌렀을 때, 팝업 창이 활성화 상태일 때 
        {
            Debug.Log($"ESC 키를 눌렀습니다! > {isEscPressed}");
            Debug.Log($"현재 팝업 상태  > {isPopUpActived}");
            _targetCanvas.gameObject.SetActive(false);
        }
    
    }

}
