using UnityEngine;

public class TouchInput : IPlayerInput
{
    public event IPlayerInput.DirectionInputEvent OnDirectionInput;

    private PlayerActions _actions;
    private Vector2 _startTouchPos;
    private float _swipeLength = 10;

    public TouchInput(PlayerActions actions)
    {
        _actions = actions;
        _actions.Touch.PrimaryTouch.started += ctx => StartTouchPrimary();
        _actions.Touch.PrimaryTouch.canceled += ctx => EndTouchPrimary();

    }

    private void StartTouchPrimary()
    {
        _startTouchPos = _actions.Touch.PrimaryPosition.ReadValue<Vector2>();
    }

    private void EndTouchPrimary()
    {
        Vector2 endPos = _actions.Touch.PrimaryPosition.ReadValue<Vector2>();
        if(IsSwipe(_startTouchPos, endPos))
        {
            var swipeDir = GetDirection(endPos - _startTouchPos);
            OnDirectionInput?.Invoke(swipeDir);
        }
    }

    private bool IsSwipe(Vector2 start, Vector2 end)
    {
        if(Vector2.Distance(start, end) > _swipeLength) return true;
        return false;
    }

    public Vector2 GetDirection(Vector2 delta)
    {
        Vector2 swipe = Vector2.zero;
        
        if(Mathf.Abs(delta.x) >= Mathf.Abs(delta.y))
        {
            swipe = delta.x < 0 ? Vector2.left : Vector2.right;
        }
        else
        {
            swipe = delta.y < 0 ? Vector2.down : Vector2.up;
        }
        
        return swipe;
    }
}