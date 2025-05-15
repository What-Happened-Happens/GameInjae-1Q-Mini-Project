using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;


public class ButtonClickEvent : MonoBehaviour
{
    [SerializeField] private Canvas uiCanvas; // Inspector 에 하나만 드래그

    private List<Button> buttons = new List<Button>(); 

    void Awake()
    {
       
    }

    public void OnButtonClicked(Button clicked)
    {
        Debug.Log($"[{clicked.name}] 버튼 클릭!");
    }
}

