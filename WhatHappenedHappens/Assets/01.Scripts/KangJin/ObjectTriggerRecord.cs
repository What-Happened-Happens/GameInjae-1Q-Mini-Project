using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTriggerRecord : MonoBehaviour
{
    public float time;
    public bool isTrue;

    //������
    public ObjectTriggerRecord(float time, bool istrue)
    {
        this.isTrue = istrue;
        this.time = time;
    }

}
