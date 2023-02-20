using UnityEngine;

[CreateAssetMenu(menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Chunks")]
    [Range(0, 20)]public int NumberOfChunks;
    [Range(0, 5)]public int NumberOfLines;
    public int StartLine;
    
    [Header("Game")]
    [Range(0, 10)]public float StartSpeed;
    [Range(0, 20)]public float SpeedMultiplier;
    [Range(0, 50)]public float SpeedIncreaseTime;
    [Range(0, 100)]public float MaxSpeed;
}