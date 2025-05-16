using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState_Player 
{
    void Enter();
    void Update();
    void FixedUpdate();
    void Exit();
}
