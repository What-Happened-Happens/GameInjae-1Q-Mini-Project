using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CircuitBreaker : TrueFalse
{
    // Start is called before the first frame update
    float elapsedTime;
    public float maxTime;
    bool isColliding = false;
    
    SpriteRenderer sr;
    public Sprite onSprites;
    public Sprite offSprites;
    void Start()
    {
        elapsedTime = 0f;
        maxTime = 5f;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = offSprites;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTrueFalse();
        ChangeSprite();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.FindWithTag("Player"))
        {
            if (!isColliding)
            {
                isColliding = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
    }

    public void CheckTrueFalse()
    {
        if (isTrue)
        {
            //Ư���ð� ������ false
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= maxTime)
            {
                Debug.Log("CircuitBreaker off!");
                isTrue = false;
                elapsedTime = 0f;
            }
        }
        else
        {
            //isTrigger üũ���϶�, Ű ������
            if (isColliding && Input.GetKey(KeyCode.F))
            {
                Debug.Log("CircuitBreaker on!");
                isTrue = true;
                elapsedTime = 0f;
            }
        }
    }
    
    public void ChangeSprite()
    {
        //��������Ʈ ��ȯ
        if(isTrue)
        {
            sr.sprite = onSprites;
        }
        else
        {
            sr.sprite = offSprites;
        }
    }
}
