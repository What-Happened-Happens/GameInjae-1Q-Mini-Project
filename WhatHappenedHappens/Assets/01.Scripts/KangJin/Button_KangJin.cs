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
        //이 녀석은 한 번 누르면 그 대로 True 값 지속 됨 -> 패러독스에만 영향 받음
        if(isColliding)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                isTrue = true;
                Debug.Log("Button On!");
            }
        }
        ChangeState();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isColliding = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
        Debug.Log("Trigger Exit!");
    }

    public void ChangeState()
    {
        if (isTrue)
        {
            sr.sprite = ButtonOn;
        }
        else
        {
            sr.sprite = ButtonOff;
        }
    }
}
