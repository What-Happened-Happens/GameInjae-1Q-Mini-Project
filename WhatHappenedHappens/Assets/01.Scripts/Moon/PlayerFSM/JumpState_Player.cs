using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState_Player : IState_Player
{
    private Player player;
    private float coyoteTime = 0.05f; // 점프 후 해당 시간동안 바닥 감지 무시
    private float elapsedTime;

    public JumpState_Player(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Jump State");
        // player.SetActiveState(Player.PlayerState.Jumping);
        player.Jump();
        elapsedTime = 0f;
    }

    public void Update()
    {
        elapsedTime += Time.deltaTime;

        bool jumpPressed = Input.GetKey(KeyCode.UpArrow);

        if (jumpPressed && player.IsGrounded() && elapsedTime >= coyoteTime)
            player.ChangeState(new JumpState_Player(player)); // 연속 점프 

        if (player.IsGrounded() && player.GetVelocity().y <= 0.01f && elapsedTime >= coyoteTime)
            player.ChangeState(new IdleState_Player(player));
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {

    }
}