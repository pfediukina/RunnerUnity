using System;
using UnityEngine;

public class JumpState : IState<Player>
{
    public void Enter(Player owner)
    {
        owner.OnPlayerSwipe += ChangeStateWithSwipe;
        Debug.Log("Jump");
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
        
    }
}

