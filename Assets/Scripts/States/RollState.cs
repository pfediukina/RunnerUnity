using System.Threading.Tasks;
using System;
using UnityEngine;

public class RollState : IState<Player>
{
    private int _animationName = Animator.StringToHash("Roll");
    private float _rollDuration = 1f;
    private float _rollTimer;

    public void Enter(Player owner)
    {
        owner.OnPlayerSwipe += ChangeStateWithSwipe;
        Roll(owner);
        _rollTimer = 0;
    }

    public void Exit(Player owner)
    {
        owner.OnPlayerSwipe -= ChangeStateWithSwipe;
    }

    public void Update(Player owner)
    {
        _rollTimer += Time.deltaTime;
        if(_rollTimer >= _rollDuration)
            owner.StateMachine.SetState<IdleState>();
    }

    private void Roll(Player owner)
    {
        owner.RigidBody.AddForce(Vector3.down * 15, ForceMode.VelocityChange);
        owner.PlayerAnimator.PlayStateAnimation(_animationName);
    }

    private void ChangeStateWithSwipe(Vector2 swipeDirection, Player owner)
    {
        if(swipeDirection == Vector2.up)
        {
            owner.StateMachine.SetState<JumpState>();
        }
        else if(swipeDirection != Vector2.down) 
        {
            owner.StateMachine.GetState<SideStepState>().
                    SetDirection(swipeDirection);
            owner.StateMachine.SetState<SideStepState>();
        }
    }
}

