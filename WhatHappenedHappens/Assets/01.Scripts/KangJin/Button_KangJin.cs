using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Button_KangJin : TrueFalse
{
    // Start is called before the first frame update
    bool isColliding = false;
    SpriteRenderer sr;
    public Sprite ButtonOn;
    public Sprite ButtonOff;
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = ButtonOff;
    }

    // Update is called once per frame
    void Update()
    {
        if(isColliding)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                isTrue = true;
                Debug.Log("Button On!");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isColliding = true;
        sr.sprite = ButtonOn;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
        sr.sprite = ButtonOff;
        Debug.Log("Trigger Exit!");
    }
}
