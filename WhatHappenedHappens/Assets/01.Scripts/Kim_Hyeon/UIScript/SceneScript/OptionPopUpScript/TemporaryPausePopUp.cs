using UnityEngine;

public class TemporaryPausePopUp : MonoBehaviour
{
    // 임시로 지정 초기화 
    public bool isActived = true; // 팝업 창 활성화 상태 
    public bool isSkip = false; // 팝업 창 스킵 상태 
    private bool isPopUpActived;
    private bool isEscPressed;

    public Canvas _targetCanvas;

    private void Start()
    {
        _targetCanvas = GetComponent<Canvas>();

        _targetCanvas.gameObject.SetActive(isActived);
        Debug.Log($"테스트를 위해서 일시적으로 활성화 상태로 지정. TemporaryPausePopUp ");
        isPopUpActived = true;
        Debug.Log($"테스트를 위해서 일시적으로 활성화 상태로 지정. TemporaryPausePopUp isPopUpActived : {isPopUpActived} ");
        isEscPressed = false;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC 키를 눌렀을 때 
        {
            isEscPressed = true;
        }
       

        TemporaryPausePopUpControl(isEscPressed);
    }

    // ESC 키 입력 상태에 따라서 팝업 창 Show 
    public void TemporaryPausePopUpControl(bool isEscPressed)
    {

        if (isEscPressed && !isPopUpActived)  // Esc키를 눌렀을 때, 팝업 창이 활성화 상태가 아닐 때 
        {
            Debug.Log($"ESC 키를 눌렀습니다! > {isEscPressed}");
            Debug.Log($"현재 팝업 상태  > {isPopUpActived}");
            _targetCanvas.gameObject.SetActive(true);
            isPopUpActived = true;
        }
        else if (isEscPressed && isPopUpActived) // Esc키를 눌렀을 때, 팝업 창이 활성화 상태일 때 
        {
            Debug.Log($"ESC 키를 눌렀습니다! > {isEscPressed}");
            Debug.Log($"현재 팝업 상태  > {isPopUpActived}");
            _targetCanvas.gameObject.SetActive(false);
            isPopUpActived = false;
        }

    }

    public void IsSkip(bool isSkip)
    {
        if (isSkip == false) return;


    }

}
