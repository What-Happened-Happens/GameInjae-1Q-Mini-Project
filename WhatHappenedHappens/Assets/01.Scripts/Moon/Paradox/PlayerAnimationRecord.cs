using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationRecord 
{
    public float time;
    public string animationState;

    public PlayerAnimationRecord(float time, string state)
    {
        this.time = time;
        this.animationState = state;
    }
}
