using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_KangJin : TrueFalse
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isTrue)
        { 
            isTrue = true;
            Debug.Log("Button On!");
        }
        else
        {
            isTrue = false;
            Debug.Log("Button Off!");
        }
    }
}
