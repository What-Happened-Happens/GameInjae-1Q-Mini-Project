using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerXor : TrueFalse
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
        LazerLeftOnOff();
        
    }
    void LazerLeftOnOff()
    {
        if (circuitBreakerLeft.IsTrue == pressureButtonMiddle)
        {//레이저 off
            isTrue = false;
        }
        else
        {//레이저 on
            isTrue = true;
        }
    }
}
