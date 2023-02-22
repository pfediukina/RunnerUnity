using System.Threading.Tasks;
using System;
using UnityEngine;

public class RollState : IState<Player>
{
    private int _animationName = Animator.StringToHash("Roll");
    private float _rollTimer;
    private float _colliderPosY = 0.5f;

    //BUG таймер не работает пока идет сайд степ 

    public void Enter(Player owner)
    {
        owner.OnPlayerInput += ChangeStateWithSwipe;
        Roll(owner);
        owner.PlayerAnimator.SetRollAnimationSpeed(owner.PlayerSettings.RollDuration);
    }

    public void Exit(Player owner)
    {
        owner.OnPlayerInput -= ChangeStateWithSwipe;
    }

    public void Update(Player owner)
    {
        _rollTimer += Time.deltaTime;
        if(_rollTimer >= owner.PlayerSettings.RollDuration)
            owner.StateMachine.SetState<IdleState>();
    }

    private void Roll(Player owner)
    {
        var colliderPos = owner.Collider.center;
        colliderPos.y = _colliderPosY;
        owner.Collider.center = colliderPos;

        _rollTimer = 0;
        owner.RigidBody.velocity = Vector3.down * owner.PlayerSettings.DropForce;
        owner.PlayerAnimator.PlayStateAnimation(_animationName);
    }

    private void ChangeStateWithSwipe(Vector2 swipeDirection, Player owner)
    {
        if(swipeDirection == Vector2.up && owner.IsGrounded)
        {
            owner.RigidBody.velocity = Vector3.zero;
            owner.StateMachine.SetState<JumpState>();
        }
    }
}
