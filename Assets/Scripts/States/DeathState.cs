using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState<Player>
{
    private int _animationNameID = Animator.StringToHash("Death");
    private Vector3 _deathPos;

    public void Enter(Player owner) {
        Death(owner);
        owner.Collider.enabled = false; 
    }

    public void Exit(Player owner) {}

    public void Update(Player owner) {}

    public void SetDeathPos(Vector3 pos)
    {
        _deathPos = pos;
    }

    private void Death(Player owner)
    {
        if(owner.PlayerAnimator != null)
            owner.PlayerAnimator.PlayStateAnimation(_animationNameID);

        GameManager.SetGameSpeed(0);
        owner.transform.eulerAngles += Vector3.down * 90;
        owner.transform.position += Vector3.back * 0.5f;
        owner.PlayerUI.ShowDeathScreen();
    }
}
