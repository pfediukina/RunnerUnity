using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations
{
    private Animator _animator;

    public PlayerAnimations(Animator animator)
    {
        _animator = animator;
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
}