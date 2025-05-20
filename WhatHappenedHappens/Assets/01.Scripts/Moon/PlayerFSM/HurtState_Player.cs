using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState_Player : IState_Player
{
    private Player player;

    public HurtState_Player(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        // Debug.Log("Hurt State");
        player.SetActiveState(Player.PlayerState.Hurt);

        player.isDead = true;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {

    }
}