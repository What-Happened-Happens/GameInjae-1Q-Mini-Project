using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState_Player : IState_Player
{
    private Player player;

    public FallState_Player(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        // Debug.Log("Fall State");
        player.SetActiveState(Player.PlayerState.Fall);
    }

    public void Update()
    {
        if(player.IsGrounded() || player.IsAccelerated() /*|| player.GetVelocity().y <= 0.01f*/ )
            player.ChangeState(new IdleState_Player(player));

    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {

    }
}
