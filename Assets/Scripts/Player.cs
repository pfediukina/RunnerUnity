using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine StateMachine { get => _stateMachine; }
    public Action<Vector2, Player> OnPlayerSwipe;

    private StateMachine _stateMachine;

    void Awake()
    {
        if(_stateMachine == null)
            _stateMachine = new StateMachine(this);
    }

    void OnEnable()
    {
        PlayerInput.OnSwipe += OnPlayerSwiped;
    }

    void OnDisable()
    {
        PlayerInput.OnSwipe -= OnPlayerSwiped;
    }

    void Update()
    {
        if(_stateMachine != null)
            _stateMachine.UpdateState();
    }

    private void OnPlayerSwiped(Vector2 direction)
    {
        OnPlayerSwipe?.Invoke(direction, this);
    }
}