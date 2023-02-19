using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static float Speed { get => _instance._speed; }
    public static int NumberOfChunks { get => _instance._numberOfChunks; }
    public static int NumberOfLines { get => _instance._numberOfLines; }
    public static int StartLine { get => _instance._startLine; }

    private float _speed;
    private int _numberOfChunks;
    private int _numberOfLines;
    private int _startLine;


    [SerializeField] private GameSettings _gameSettings;

    private void Awake()
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
        
        _speed = _gameSettings.StartSpeed;
        _numberOfChunks = _gameSettings.NumberOfChunks;
        _numberOfLines = _gameSettings.NumberOfLines;
        _startLine = _gameSettings.StartLine;
    }
}