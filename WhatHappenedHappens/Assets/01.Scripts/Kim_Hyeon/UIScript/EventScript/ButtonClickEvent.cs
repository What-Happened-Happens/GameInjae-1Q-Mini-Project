using UnityEngine;
public class ButtonClickEvent : MonoBehaviour
{  
    public void OnButtonClicked()// 버튼 클릭 시에 들어갈 이벤트 
    {
        Debug.Log($"[{gameObject.name}] 버튼 클릭!");
    }

    public void OnMouseUpAsButton()
    {
        Debug.Log($"[{gameObject.name}] 버튼 클릭 종료!");
    }
}

