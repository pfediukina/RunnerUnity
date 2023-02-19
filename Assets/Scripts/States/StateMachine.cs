using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState<Player> CurrentState { get => _currentState;}
    public IState<Player> PreviousState { get => _previousState;}

    private Player _player;
    private Dictionary<Type, IState<Player>> _statesMap;
    private IState<Player> _currentState;
    private IState<Player> _previousState;

    public StateMachine(Player player)
    {
        _player = player;
        Initialize();
    } 

    public void UpdateState()
    {
        if(_currentState != null)
            _currentState.Update(_player);
    }

    private void Initialize()
    {
        InitStates();
        SetBehaviourByDefault();
    }

    public void SetState<T>() where T : IState<Player>
    {
        SetState(GetState<T>());
    }

    public T GetState<T>() where T : IState<Player>
    {
        var type = typeof(T); //NEED TEST
        return (T)_statesMap[type];
    }

    private void InitStates()
    {
        _statesMap = new Dictionary<Type, IState<Player>>();

        _statesMap[typeof(IdleState)] = new IdleState();
        _statesMap[typeof(JumpState)] = new JumpState();
        _statesMap[typeof(RollState)] = new RollState();
        _statesMap[typeof(StepState)] = new StepState();
    }

    private void SetBehaviourByDefault()
    {
        var defaultState = GetState<IdleState>();
        SetState(defaultState);
    }

    private void SetState(IState<Player> newState)
    {
        if(_currentState != null)
            _currentState.Exit(_player);

        _previousState = _currentState;
        _currentState = newState;
        newState.Enter(_player);
    }
}