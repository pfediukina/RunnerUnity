using System.Collections;
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
    private Collider _obstacle;

    [SerializeField] private Transform _groundChecker;

    void Awake()
    {
        OnPlayerInput = null;
        OnPlayerUpdate = null;
    }

    private void Start()
    {
        InitializeComponents();
    }

    private void OnEnable()
    {
        PlayerInput.OnInput += Input_OnPlayerInput;
        Advertising.OnPlayerSawAd += OnPlayerSawAd;
    }

    private void OnDisable()
    {
        PlayerInput.OnInput -= Input_OnPlayerInput;
        Advertising.OnPlayerSawAd -= OnPlayerSawAd;
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
        StateMachine.SetState<DeathState>();
        _obstacle = other;
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
        //Debug.Log("HERE");
        OnPlayerInput?.Invoke(direction, this);
    }

    private void OnPlayerSawAd()
    {
        _obstacle.gameObject.SetActive(false);
        _collider.enabled = true;

        StartCoroutine(SetPlayerIdleAfterPause());
        //GameLifetime.ResumeGame();
    }

    private IEnumerator SetPlayerIdleAfterPause()
    {
        yield return new WaitForSeconds(3);
        StateMachine.SetState<IdleState>();
        transform.rotation = Quaternion.identity;
    }
}