using System;
using UnityEngine;

public class StepState : IState<Player>
{
    private bool _isRight;
    private float _stepTimer;
    private int _line = -1;
    private float _delta = 3.5f;

    private float[] _linesPosX;

    public void Enter(Player owner)
    {
        if(_line == -1)
        {
            _line = GameManager.StartLine;
           SetLinesPositionX(owner);   
        }
        
        _delta = Mathf.Abs(_delta);
        Side(owner);
    }

    public void Exit(Player owner) {}

    public void Update(Player owner)
    {
        _stepTimer += Time.deltaTime;
        //var newX = _delta * Time.deltaTime / owner.PlayerSettings.StepDuration;
        float offset = Mathf.Lerp(owner.transform.position.x, _linesPosX[_line], _stepTimer / owner.PlayerSettings.StepDuration);
        owner.transform.position = new Vector3(offset, owner.transform.position.y, owner.transform.position.z);

        if(_stepTimer >= owner.PlayerSettings.StepDuration)
        {
            SetPreviousState(owner);
        }
    }

    public void SetDirection(Vector2 dir)
    {
        _isRight = dir == Vector2.right ? true : false;
    }

    public void Side(Player owner)
    {
        _stepTimer = 0;

        var lastLine = _line; 
        _line += _isRight ? 1 : -1; 
        _line = Mathf.Clamp(_line, 0, GameManager.NumberOfLines - 1);

        if(lastLine == _line)
        {
            SetPreviousState(owner);
            return;
        }

        _delta *= _line - lastLine;
    }

    private void SetPreviousState(Player owner)
    {
        if(owner.StateMachine.PreviousState is RollState)
            owner.StateMachine.SetState<RollState>();
        else if(owner.IsGrounded)
            owner.StateMachine.SetState<IdleState>();
        else 
            owner.StateMachine.SetState<JumpState>();
    }

    private void SetLinesPositionX(Player owner)
    {
        _linesPosX = new float[GameManager.NumberOfLines]; 
        _linesPosX[GameManager.StartLine] = owner.transform.position.x;

        for(int i = 0; i < GameManager.NumberOfLines; i++)
        {
            if(i == GameManager.StartLine) continue;
            _linesPosX[i] = _delta * (i - GameManager.StartLine) + _linesPosX[GameManager.StartLine];
        }
    }
}