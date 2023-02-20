using UnityEngine;

public class TouchInput : IPlayerInput
{
    public Vector2 GetDirection(Vector2 direction)
    {
        Vector2 swipe = Vector2.zero;
        if(Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
        {
            swipe = direction.x < 0 ? Vector2.left : Vector2.right;
        }
        else
        {
            swipe = direction.y < 0 ? Vector2.down : Vector2.up;
        }
        return swipe;
    }
}