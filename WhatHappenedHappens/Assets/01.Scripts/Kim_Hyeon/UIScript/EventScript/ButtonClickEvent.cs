using UnityEngine;
public class ButtonClickEvent : MonoBehaviour
{  
    public void OnButtonClicked()// ��ư Ŭ�� �ÿ� �� �̺�Ʈ 
    {
        Debug.Log($"[{gameObject.name}] ��ư Ŭ��!");
    }

    public void OnMouseUpAsButton()
    {
        Debug.Log($"[{gameObject.name}] ��ư Ŭ�� ����!");
    }
}

