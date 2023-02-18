using System;
using UnityEngine;

public class IdleState : IState<Player>
{
    public void Enter(Player owner)
    {
        owner.OnPlayerSwipe += ChangeStateWithSwipe;
    }

    public void Exit(Player owner)
    {
        owner.OnPlayerSwipe -= ChangeStateWithSwipe;
    }

    public void Update(Player owner)
    {
        
    }

    private void ChangeStateWithSwipe(Vector2 swipeDirection, Player player)
    {
        if(swipeDirection == Vector2.up)
        {
            player.StateMachine.SetState<JumpState>(); //отошла
        }
    }
}

