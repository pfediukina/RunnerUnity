using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody RigidBody { get => _rb; }
    public StateMachine StateMachine { get => _stateMachine; }
    public PlayerAnimations PlayerAnimator { get => _animator; }
    public PlayerSettings PlayerSettings { get => _settings; }
    public bool IsGrounded => Physics.CheckSphere(_groundChecker.position, 0.1f, PlayerSettings.GroundLayer);

    public Action<Vector2, Player> OnPlayerMove;
    
    
    [SerializeField] private PlayerSettings _settings;

    private StateMachine _stateMachine;
    private PlayerAnimations _animator;
    private PlayerLine _line;
    private Rigidbody _rb;

    [SerializeField] private Transform _groundChecker;

    private void Awake()
    {
        InitializeComponents();
    }

    private void OnEnable()
    {
        PlayerInput.OnMove += OnMoved;
    }

    private void OnDisable()
    {
        PlayerInput.OnMove -= OnMoved;
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
        
        _line = new PlayerLine(this);

        _rb = GetComponent<Rigidbody>();
    }

    private void OnMoved(Vector2 direction)
    {
        OnPlayerMove?.Invoke(direction, this);
    }
}