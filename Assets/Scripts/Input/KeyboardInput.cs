using UnityEngine;

public class KeyboardInput : IPlayerInput
{
    public Vector2 GetDirection(Vector2 direction)
    { 
        var d = Vector2.zero;
        if(direction.x != 0)
        {
            d = direction.x < 0 ? Vector2.left : Vector2.right;
        }
        else if(direction.y != 0)
        {
            d = direction.y < 0 ? Vector2.down : Vector2.up;
        }
        return d;
    }
}