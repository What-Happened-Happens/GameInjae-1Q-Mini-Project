using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState_Player : IState_Player
{
    private Player player;

    public IdleState_Player(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        // Debug.Log("Idle State");
        player.SetActiveState(Player.PlayerState.Idle);
    }

    public void Update()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
            player.ChangeState(new WalkState_Player(player));

        if (Input.GetKeyDown(KeyCode.UpArrow) && player.IsGrounded())
            player.ChangeState(new JumpState_Player(player));
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {

    }
}
