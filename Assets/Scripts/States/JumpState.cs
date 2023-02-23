using System.Threading.Tasks;
using System;
using UnityEngine;

public class JumpState : IState<Player>
{
    private int _animationStartID = 0;
    private int _animationLoopID = 0;
    private float _colliderPosY = 2f;

    public void Enter(Player owner)
    {
        owner.OnPlayerInput += ChangeStateWithSwipe;
        owner.Collider.center += Vector3.up * 1.5f;
        if(_animationStartID == 0)
        {
            _animationStartID = Animator.StringToHash(owner.PlayerSettings.AnimationNames.JumpStart);
            _animationLoopID = Animator.StringToHash(owner.PlayerSettings.AnimationNames.JumpLoop);
        }
        Jump(owner);
    }

    public void Exit(Player owner)
    {
        owner.OnPlayerInput -= ChangeStateWithSwipe;
    }

    public void Update(Player owner)
    {
        owner.PlayerAnimator.SetPlayerSpeedY(owner.RigidBody.velocity.y);
        if(owner.IsGrounded && owner.RigidBody.velocity.y < 0)
        {
            owner.StateMachine.SetState<IdleState>();
        }
    }

    private void Jump(Player owner)
    {
        var colliderPos = owner.Collider.center;
        colliderPos.y = _colliderPosY;
        owner.Collider.center = colliderPos;

        var rb = owner.RigidBody;
        if(rb != null)
        {
            rb.velocity = Vector3.up * owner.PlayerSettings.JumpForce;
            owner.PlayerAnimator.PlayStateAnimation(_animationStartID);
        }
    }

    private void ChangeStateWithSwipe(Vector2 swipeDirection, Player owner)
    {
        if(swipeDirection == Vector2.down)
            owner.StateMachine.SetState<RollState>();
    }
}