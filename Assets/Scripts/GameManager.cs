using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Singelton =>_instance;
    public static GameSettings GameSettings => _instance._gameSettings;

    public static Action OnGameSpeedChanged;

    public static float Speed => _instance._speed; 
    public static float Score => _instance._score; 

    private float _speed;
    private float _score = 0;
    private Coroutine _speedIncrease;

    [SerializeField] private GameSettings _gameSettings;

    private void Awake()
    {
        if(_instance == null)
            _instance = this;
        Initialize();
    }

    private void Start()
    {
        _speedIncrease = StartCoroutine(IncreaseSpeed());
    }

    private void Update()
    {
        _score += Time.deltaTime * Speed;
    }

    public static void GoToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public static void SetGameSpeed(float speed)
    {
        _instance.StopCoroutine(_instance._speedIncrease);
        _instance._speed = speed;
    }

    private void Initialize()
    {
        if(_gameSettings == null)
        {
            Debug.LogError("GameSettings was not found");
            return;
        }
        
        _score = 0;
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