using System;
using UnityEngine;

public class IdleState : IState<Player>
{
    private int _animationName = Animator.StringToHash("Idle");

    public void Enter(Player owner)
    {
        owner.OnPlayerSwipe += ChangeStateWithSwipe;
        Idle(owner);
    }

    public void Exit(Player owner)
    {
        owner.OnPlayerSwipe -= ChangeStateWithSwipe;
    }

    public void Update(Player owner)
    {
        
    }

    private void Idle(Player owner)
    {
        if(owner.PlayerAnimator != null)
            owner.PlayerAnimator.PlayStateAnimation(_animationName);
    }

    private void ChangeStateWithSwipe(Vector2 swipeDirection, Player owner)
    {
        if(swipeDirection == Vector2.up)
            owner.StateMachine.SetState<JumpState>();
        else if(swipeDirection == Vector2.down)
            owner.StateMachine.SetState<RollState>();
        else
        {
            owner.StateMachine.GetState<StepState>().
                    SetDirection(swipeDirection);
            owner.StateMachine.SetState<StepState>();
        }
    }
}

