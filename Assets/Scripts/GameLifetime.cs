using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLifetime : MonoBehaviour
{
    private static GameLifetime _instance;

    public static Action OnGameSpeedChanged;

    public static float Speed => _instance._speed; 
    public static float Score => _instance._score; 

    private float _speed;
    private float _score = 0;
    private Coroutine _speedIncrease;

    private void Awake()
    {
        if(_instance == null)
            _instance = this;

        Initialize();
    }

    private void Update()
    {
        _score += Time.deltaTime * Speed;
    }

    private void Start()
    {
        _speedIncrease = StartCoroutine(IncreaseSpeed());
    }

    public static void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public static void SetGameSpeed(float speed)
    {
        _instance.StopCoroutine(_instance._speedIncrease);
        _instance._speed = speed;
    }

    private IEnumerator IncreaseSpeed()
    {
        while(_speed < GameData.GameSettings.MaxSpeed)
        {
            yield return new WaitForSeconds(GameData.GameSettings.SpeedIncreaseTime);
            _speed += GameData.GameSettings.SpeedMultiplier;
            
            OnGameSpeedChanged?.Invoke();
        }
    }

    private void Initialize()
    {
        _score = 0;
        if(PlayerData.Score != 0)
        {
            _score = PlayerData.Score;
        }
        _speed = GameData.GameSettings.StartSpeed;
    }
}