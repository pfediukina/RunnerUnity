using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static float Speed { get => _instance._speed; }
    public static int NumberOfChunks { get => _instance._numberOfChunks; }

    private float _speed;
    private int _numberOfChunks;


    [SerializeField] private GameSettings _gameSettings;

    void Awake()
    {
        if(_instance == null)
            _instance = this;
        Initialize();
    }

    private void Initialize()
    {
        if(_gameSettings == null)
        {
            Debug.LogError("GameSettings was not found");
            return;
        }

        _numberOfChunks = _gameSettings.NumberOfChunks;
        _speed = _gameSettings.StartSpeed;
    }
}