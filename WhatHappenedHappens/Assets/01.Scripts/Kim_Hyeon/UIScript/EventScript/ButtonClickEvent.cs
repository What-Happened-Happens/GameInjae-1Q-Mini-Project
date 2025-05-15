using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;


public class ButtonClickEvent : MonoBehaviour
{
    [SerializeField] private Canvas uiCanvas; // Inspector �� �ϳ��� �巡��

    private List<Button> buttons = new List<Button>(); 

    void Awake()
    {
       
    }

    public void OnButtonClicked(Button clicked)
    {
        Debug.Log($"[{clicked.name}] ��ư Ŭ��!");
    }
}

