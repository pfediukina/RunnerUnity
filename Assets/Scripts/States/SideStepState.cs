using System;
using UnityEngine;

public class SideStepState : IState<Player>
{
    private bool _isRight;
    private float _stepDuration = 0.5f;
    private float _stepTimer;
    private int _line = -1;
    private float _delta = 3.5f;

    public void Enter(Player owner)
    {
        if(_line == -1)
        {
            _line = GameManager.StartLine;
        }
        
        _delta = Mathf.Abs(_delta);
        Side(owner);
    }

    public void Exit(Player owner)
    {
    }

    public void Update(Player owner)
    {
        _stepTimer += Time.deltaTime;
        var newZ = _delta * Time.deltaTime / _stepDuration;
        owner.transform.position += Vector3.back * newZ;
        Debug.Log(_delta);

        if(_stepTimer >= _stepDuration)
        {
                owner.StateMachine.SetState<IdleState>();
        }
    }

    public void SetDirection(Vector2 dir)
    {
        _isRight = dir == Vector2.right ? true : false;
    }

    public void Side(Player owner)
    {
        _stepTimer = 0;

        var lastLine = _line; //0
        _line += _isRight ? 1 : -1; //-1
        _line = Mathf.Clamp(_line, 0, GameManager.NumberOfLines - 1);

        //Debug.Log(lastLine + " -> " + _line);
        if(lastLine == _line)
        {
            owner.StateMachine.SetState<IdleState>();
            return;
        }

        _delta *= _line - lastLine;
    }
}