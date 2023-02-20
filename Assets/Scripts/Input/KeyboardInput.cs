using UnityEngine;

public class KeyboardInput : IPlayerInput
{
    public event IPlayerInput.DirectionInputEvent OnDirectionInput;
    private PlayerActions _actions;

    public KeyboardInput(PlayerActions actions)
    {
        _actions = actions;
        _actions.Keyboard.WASD.performed += ctx => GetDirection(_actions.Keyboard.WASD.ReadValue<Vector2>());
    }

    public void GetDirection(Vector2 direction)
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
        OnDirectionInput?.Invoke(d);
    }
}