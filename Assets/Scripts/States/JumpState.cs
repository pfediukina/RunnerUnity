using System.Threading.Tasks;
using System;
using UnityEngine;

public class JumpState : IState<Player>
{
    private int _animationStartID = Animator.StringToHash("JumpStart");
    private int _animationLoopID = Animator.StringToHash("JumpLoop");

    public void Enter(Player owner)
    {
        owner.OnPlayerInput += ChangeStateWithSwipe;
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
            Debug.Log("true");
            owner.StateMachine.SetState<IdleState>();
        }
    }

    private void Jump(Player owner)
    {
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