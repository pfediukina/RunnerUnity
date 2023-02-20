using System;
using UnityEngine;

public class IdleState : IState<Player>
{
    private int _animationName = Animator.StringToHash("Idle");
    private float _colliderPosY = 1.3f;

    public void Enter(Player owner)
    {
        owner.OnPlayerInput += ChangeStateWithSwipe;
        Idle(owner);
    }

    public void Exit(Player owner)
    {
        owner.OnPlayerInput -= ChangeStateWithSwipe;
    }

    public void Update(Player owner)
    {
        
    }

    private void Idle(Player owner)
    {
        var colliderPos = owner.Collider.center;
        colliderPos.y = _colliderPosY;
        owner.Collider.center = colliderPos;

        if(owner.PlayerAnimator != null)
            owner.PlayerAnimator.PlayStateAnimation(_animationName);
    }

    private void ChangeStateWithSwipe(Vector2 swipeDirection, Player owner)
    {
        if(swipeDirection == Vector2.up)
            owner.StateMachine.SetState<JumpState>();
        else if(swipeDirection == Vector2.down)
            owner.StateMachine.SetState<RollState>();
    }
}

