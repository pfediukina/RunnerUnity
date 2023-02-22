using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    private static GameData _instance;

    public static GameData Singelton =>_instance;
    public static GameSettings GameSettings => _instance._gameSettings;

    public static Action OnGameSpeedChanged;

    public static bool IsWatchedAd = false; 

    private Coroutine _speedIncrease;

    [SerializeField] private GameSettings _gameSettings;

    private void Awake()
    {
        if(_instance == null)
            _instance = this;
        Initialize();
    }

    private void Initialize()
    {
        IsWatchedAd = false;
        if(_gameSettings == null)
        {
            Debug.LogError("GameSettings was not found");
            return;
        }
    }
}