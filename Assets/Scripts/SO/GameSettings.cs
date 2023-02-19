using UnityEngine;

[CreateAssetMenu(menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Chunks")]
    public int NumberOfChunks;
    public int NumberOfLines;
    public int StartLine;
    
    [Header("Game")]
    public float StartSpeed;
    public float SpeedMultiplier;
    public float SpeedIncreaseTime;
    public float MaxSpeed;
}