using System.Threading.Tasks;
using System;
using UnityEngine;

public class RollState : IState<Player>
{
    private int _animationName = Animator.StringToHash("Roll");
    private float _rollTimer;

    //BUG таймер не работает пока идет сайд степ 

    public void Enter(Player owner)
    {
        owner.OnPlayerMove += ChangeStateWithSwipe;
        Roll(owner);
        owner.PlayerAnimator.SetRollAnimationSpeed(owner.PlayerSettings.RollDuration);
    }

    public void Exit(Player owner)
    {
        owner.OnPlayerMove -= ChangeStateWithSwipe;
    }

    public void Update(Player owner)
    {
        _rollTimer += Time.deltaTime;
        if(_rollTimer >= owner.PlayerSettings.RollDuration)
            owner.StateMachine.SetState<IdleState>();
    }

    private void Roll(Player owner)
    {
        _rollTimer = 0;
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
    }
}
