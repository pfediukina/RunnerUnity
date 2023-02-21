using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody RigidBody => _rb; 
    public BoxCollider Collider  => _collider; 
    public StateMachine StateMachine  => _stateMachine; 
    public PlayerAnimations PlayerAnimator => _animator; 
    public PlayerSettings PlayerSettings => _settings;
    public PlayerUI PlayerUI => _playerUI;
    public bool IsGrounded => Physics.CheckSphere(_groundChecker.position, 0.1f, PlayerSettings.GroundLayer);

    public Action<Vector2, Player> OnPlayerInput;
    public Action<Player> OnPlayerUpdate;    
    
    [SerializeField] private PlayerSettings _settings;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private PlayerUI _playerUI;

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
        PlayerInput.OnInput += Input_OnPlayerInput;
    }

    private void OnDisable()
    {
        PlayerInput.OnInput -= Input_OnPlayerInput;
    }

    private void Update()
    {
        if(_stateMachine != null)
            _stateMachine.UpdateState();
        if(_line != null)
            _line.UpdateLine(this);
    }

    void OnTriggerEnter(Collider other)
    {
        var death = StateMachine.GetState<DeathState>();
        death.SetDeathPos(other.transform.position);
        StateMachine.SetState<DeathState>();
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

    private void Input_OnPlayerInput(Vector2 direction)
    {
        OnPlayerInput?.Invoke(direction, this);
    }
}