using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Player _player;
    private Dictionary<Type, IState<Player>> _statesMap;
    private IState<Player> _currentState;

    public StateMachine(Player player)
    {
        _player = player;
    } 

    public void UpdateState()
    {
        _currentState.Update(_player);
    }

    private void Initialize()
    {
        InitStates();
        SetBehaviourByDefault();
    }

    private void InitStates()
    {
        _statesMap = new Dictionary<Type, IState<Player>>();

        _statesMap[typeof(IdleState)] = new IdleState();
        _statesMap[typeof(JumpState)] = new JumpState();
    }

    private void SetBehaviourByDefault()
    {
        var defaultState = GetState<IdleState>();
        SetState(defaultState);
    }

    private void SetState<T>() where T : IState<Player>
    {
        SetState(GetState<T>());
    }

    private void SetState(IState<Player> newState)
    {
        if(_currentState != null)
            _currentState.Exit(_player);

        _currentState = newState;
        _currentState.Enter(_player);
    }

    private IState<Player> GetState<T>() where T : IState<Player>
    {
        var type = typeof(T);
        return _statesMap[type];
    }
}