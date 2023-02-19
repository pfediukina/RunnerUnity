using System.Threading.Tasks;
using System;
using UnityEngine;

public class JumpState : IState<Player>
{
    private int _animationName = Animator.StringToHash("JumpStart");

    private float _jumpForce = 15;

    public void Enter(Player owner)
    {
        owner.OnPlayerSwipe += ChangeStateWithSwipe;
        Jump(owner);
    }

    public void Exit(Player owner)
    {
        owner.OnPlayerSwipe -= ChangeStateWithSwipe;
    }

    public void Update(Player owner)
    {
        owner.PlayerAnimator.SetPlayerSpeedY(owner.RigidBody.velocity.y);
        //Debug.Log(owner.IsGrounded);
        if(owner.IsGrounded && owner.RigidBody.velocity.y < 0)
        {
            owner.StateMachine.SetState<IdleState>();
        }
    }

    private void Jump(Player owner)
    {
        var rb = owner.RigidBody;
        
        if(rb != null)
        {
            rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
            owner.PlayerAnimator.PlayStateAnimation(_animationName);
        }
    }

    private void ChangeStateWithSwipe(Vector2 swipeDirection, Player owner)
    {
        if(swipeDirection == Vector2.down)
            owner.StateMachine.SetState<RollState>();
        
        // else if(swipeDirection != Vector2.up) 
        // {
        //     owner.StateMachine.GetState<SideStepState>().
        //             SetDirection(swipeDirection);
        //     owner.StateMachine.SetState<SideStepState>();
        // }
    }
}

