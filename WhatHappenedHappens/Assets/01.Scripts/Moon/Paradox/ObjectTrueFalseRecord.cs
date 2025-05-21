using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrueFalseRecord : MonoBehaviour
{
    public float time;
    public bool state;

    public ObjectTrueFalseRecord(float time, bool state)
    {
        this.time = time;
        this.state = state;
    }
}
