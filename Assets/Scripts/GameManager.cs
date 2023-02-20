using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Singelton =>_instance;
    public static GameSettings GameSettings => _instance._gameSettings;

    public static Action OnGameSpeedChanged;


    //wip
    public static float Speed { get => _instance._speed; }

    private float _speed;

    [SerializeField] private GameSettings _gameSettings;

    private void Awake()
    {
        if(_instance == null)
            _instance = this;
        Initialize();
    }

    private void Start()
    {
        StartCoroutine(IncreaseSpeed());
    }


    private void Initialize()
    {
        if(_gameSettings == null)
        {
            Debug.LogError("GameSettings was not found");
            return;
        }
        
        _speed = _gameSettings.StartSpeed;
    }

    private IEnumerator IncreaseSpeed()
    {
        while(_speed < _gameSettings.MaxSpeed)
        {
            yield return new WaitForSeconds(_gameSettings.SpeedIncreaseTime);
            _speed += _gameSettings.SpeedMultiplier;
            
            OnGameSpeedChanged?.Invoke();
        }
    }
}