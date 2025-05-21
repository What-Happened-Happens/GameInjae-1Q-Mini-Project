using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState_Player : IState_Player
{
    private Player player;

    public WalkState_Player(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        // Debug.Log("Walk State");
        player.SetActiveState(Player.PlayerState.Walking);
        SoundManager.Instance.PlayLoopSFX("Walk", 2.0f);
    }

    public void Update()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0)
            player.ChangeState(new IdleState_Player(player));

        if (Input.GetKeyDown(KeyCode.UpArrow) && (player.IsGrounded() || player.IsAccelerated()))
            player.ChangeState(new JumpState_Player(player));

        // [ 공중 상태 ] 
        if (!player.IsGrounded() && !player.IsAccelerated())
            player.ChangeState(new FallState_Player(player));

        if (player.isDead)
            player.ChangeState(new HurtState_Player(player));
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {
        SoundManager.Instance.StopLoopSFX();
    }
}
