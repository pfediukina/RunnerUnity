using System.Threading.Tasks;
using System;
using UnityEngine;

public class RollState : IState<Player>
{
    private int _animationName = Animator.StringToHash("Roll");
    private float _rollTimer;
    private float _previousEnterTime = -1;

    //BUG таймер не работает пока идет сайд степ 

    public void Enter(Player owner)
    {
        owner.OnPlayerSwipe += ChangeStateWithSwipe;
        if(_previousEnterTime == -1)
            _previousEnterTime = Time.time;
        Roll(owner);
        owner.PlayerAnimator.SetRollAnimationSpeed(owner.PlayerSettings.RollDuration);
    }

    public void Exit(Player owner)
    {
        owner.OnPlayerSwipe -= ChangeStateWithSwipe;
    }

    public void Update(Player owner)
    {
        _rollTimer += Time.deltaTime;
        if(_rollTimer >= owner.PlayerSettings.RollDuration)
            owner.StateMachine.SetState<IdleState>();
    }

    private void Roll(Player owner)
    {
        if(owner.StateMachine.PreviousState is not StepState)
        {
            _rollTimer = 0;
            _previousEnterTime = -1;
        }
        else
        {
            if(_previousEnterTime != -1)
            {
                if(Time.time - _previousEnterTime > owner.PlayerSettings.RollDuration)
                {
                    Debug.Log ("End");
                   // _rollTimer =   wner.PlayerSettings.RollDuration
                    owner.StateMachine.SetState<IdleState>();
                }
            }
        }

        owner.RigidBody.AddForce(Vector3.down * owner.PlayerSettings.DropForce, ForceMode.VelocityChange);
        owner.PlayerAnimator.PlayStateAnimation(_animationName);
    }

    private void ChangeStateWithSwipe(Vector2 swipeDirection, Player owner)
    {
        if(swipeDirection == Vector2.up && owner.IsGrounded)
        {
            owner.StateMachine.SetState<JumpState>();
            owner.RigidBody.velocity = Vector3.zero;
        }
        else if(swipeDirection == Vector2.right || swipeDirection == Vector2.left) 
        {
            owner.StateMachine.GetState<StepState>().
                    SetDirection(swipeDirection);
            owner.StateMachine.SetState<StepState>();
            //Debug.Log(swipeDirection);
        }
    }
}
