using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;



public class ButtonClickEvent : MonoBehaviour
{  
     
    public void OnButtonClicked()// ��ư Ŭ�� �ÿ� �� �̺�Ʈ 
    {
        Debug.Log($"[{gameObject.name}] ��ư Ŭ��!");
    }
}

