using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAnd : TrueFalse
{
    public TrueFalse circuitBreakerLeft;
    public TrueFalse pressureButtonMiddle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LazerRightOnOff();
    }
    void LazerRightOnOff()
    {
        if (circuitBreakerLeft.IsTrue == true || pressureButtonMiddle.IsTrue == true)
        { // ������ off
            isTrue = true;
        }
        else
        {//  ������ on
            isTrue = false;
        }
    }
}
