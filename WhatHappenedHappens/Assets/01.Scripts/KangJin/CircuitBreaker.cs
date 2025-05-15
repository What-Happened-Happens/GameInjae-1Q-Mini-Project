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
    void Start()
    {
        elapsedTime = 0f;
        maxTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrue)
        {
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
            if (isColliding && Input.GetKey(KeyCode.B))
            {
                Debug.Log("CircuitBreaker on!");
                isTrue = true;
                elapsedTime = 0f;
            }
        }
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
}
