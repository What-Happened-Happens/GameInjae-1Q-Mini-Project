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
        //�� �༮�� �� �� ������ �� ��� True �� ���� �� -> �з��������� ���� ����
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
