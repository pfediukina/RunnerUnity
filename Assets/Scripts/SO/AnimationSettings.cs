using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/AnimationSettings")]
public class AnimationSettings : ScriptableObject
{
    [Header("Amimation names")]
    public string Idle;
    public string Standing;
    public string JumpStart;
    public string JumpLoop;
    public string JumpEnd;
    public string Roll;
    public string Death;
}