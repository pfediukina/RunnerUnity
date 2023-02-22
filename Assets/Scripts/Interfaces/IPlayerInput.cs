using System;
using UnityEngine;

public interface IPlayerInput
{
    public delegate void DirectionInputEvent(Vector2 direction);
    public event DirectionInputEvent OnDirectionInput;
    //public Vector2 GetDirection(Vector2 direction);
}
