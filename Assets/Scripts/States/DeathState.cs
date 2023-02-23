using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState<Player>
{
    private int _animationNameID = 0;
    
    public void Enter(Player owner) 
    {
        if(_animationNameID == 0)
            _animationNameID = Animator.StringToHash(owner.PlayerSettings.AnimationNames.Death);
            
        Death(owner);
        owner.Collider.enabled = false; 
    }

    public void Exit(Player owner) {}

    public void Update(Player owner) {}

    private void Death(Player owner)
    { 
        owner.PlayerAnimator.PlayStateAnimation(_animationNameID);
        GameLifetime.PauseGame();
        owner.transform.eulerAngles += Vector3.down * 90;
        owner.transform.position += Vector3.back * 0.5f;
        owner.PlayerUI.ShowDeathScreen(true);
    }
}
