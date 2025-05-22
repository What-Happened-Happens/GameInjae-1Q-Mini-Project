using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSensor : TrueFalse
{
    // Start is called before the first frame update
    float elapsedTime;
    public float limitTime;
    bool timeCheck = false;
    void Start()
    {
        elapsedTime = 0;
        limitTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeCheck)
        {
            elapsedTime += Time.deltaTime;
        }
        if(elapsedTime >=limitTime)
        {
            isTrue = false;
            timeCheck = false;
            elapsedTime = 0;
            Debug.Log("Sensor off!");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        isTrue = true;
        elapsedTime = 0;
        Debug.Log("Keep Pressing!");
        Debug.Log(gameObject.transform.up);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        timeCheck = true;
        Debug.Log("Pressured off!");
    }
}
