using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Physics")]
    [Range(0, 50)]public float JumpForce;
    [Range(0, 50)]public float DropForce;
    public LayerMask GroundLayer;
    
    [Header("Animation")]
    [Range(0, 5)]public float StepDuration;
    [Range(0, 5)]public float RollDuration;
}
