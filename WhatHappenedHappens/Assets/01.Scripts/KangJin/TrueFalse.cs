using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueFalse : MonoBehaviour
{
    protected bool isTrue = false;
    public bool IsTrue { get { return isTrue; } }

    public virtual void SetState(bool value)
    {
        isTrue = value;
    }
}
