using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody RigidBody { get => _rb; }
    public StateMachine StateMachine { get => _stateMachine; }
    public PlayerAnimations PlayerAnimator { get => _animator; }
    public bool IsGrounded => Physics.CheckSphere(_groundChecker.position, 0.1f, mask);

    public Action<Vector2, Player> OnPlayerSwipe;

    private StateMachine _stateMachine;
    private PlayerAnimations _animator;
    private Rigidbody _rb;

    public LayerMask mask; //test

    [SerializeField] private Transform _groundChecker;

    private void Awake()
    {
        InitializeComponents();
    }

    private void OnEnable()
    {
        PlayerInput.OnSwipe += OnPlayerSwiped;
    }

    private void OnDisable()
    {
        PlayerInput.OnSwipe -= OnPlayerSwiped;
    }

    private void Update()
    {
        if(_stateMachine != null)
            _stateMachine.UpdateState();
    }


    private void InitializeComponents()
    {
        if(_animator == null)
            _animator = new PlayerAnimations(GetComponentInChildren<Animator>());
        
        if(_stateMachine == null)
            _stateMachine = new StateMachine(this);
        
        _rb = GetComponent<Rigidbody>();
    }

    private void OnPlayerSwiped(Vector2 direction)
    {
        OnPlayerSwipe?.Invoke(direction, this);
    }
}