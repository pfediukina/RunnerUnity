using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations
{
    private Animator _animator;

    public PlayerAnimations(Animator animator)
    {
        _animator = animator;
        GameLifetime.OnGameSpeedChanged += SetIdleAnimationSpeed;
        SetIdleAnimationSpeed(1);
    }

    public void PlayStateAnimation(int animationID)
    {
        if(_animator != null)
        {
            _animator.CrossFade(animationID, 0.1f);
        }
    }

    public void SetPlayerSpeedY(float speed)
    {
        _animator.SetFloat("SpeedY", speed);
    }

    public void SetRollAnimationSpeed(float speed)
    {
        var s = 1 / speed;
        _animator.SetFloat("RollSpeed", s);
    }

    public void SetIdleAnimationSpeed(float speed)
    {
        var s = speed / GameData.GameSettings.MaxSpeed + 1;
        _animator.SetFloat("IdleSpeed", s);
    }

    public void SetIdleAnimationSpeed()
    {
        SetIdleAnimationSpeed(GameLifetime.Speed);
    }

}