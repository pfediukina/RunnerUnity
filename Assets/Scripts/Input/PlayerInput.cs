using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInput : MonoBehaviour
{
    public static Action<Vector2> OnMove;

    private PlayerActions _actions;
    private Vector2 _startTouchPos;
    private float _swipeLength = 10;

    private void Awake()
    {
        if(_actions == null)
            _actions = new PlayerActions();
    }

    private void OnEnable()
    {
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }

    private void Start()
    {
        Bind();
    }

    private void Bind()
    {
        _actions.Touch.PrimaryTouch.started += ctx => StartTouchPrimary(ctx);
        _actions.Touch.PrimaryTouch.canceled += ctx => EndTouchPrimary(ctx);

        _actions.Keyboard.WASD.performed += ctx => GetKeyDirection(_actions.Keyboard.WASD.ReadValue<Vector2>());
    }

    private void GetKeyDirection(Vector2 direction)
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
        OnMove?.Invoke(d);
    }

    private void InvokeDirection(Vector2 direction)
    {
        OnMove?.Invoke(direction);
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        _startTouchPos = _actions.Touch.PrimaryPosition.ReadValue<Vector2>();
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        Vector2 endPos = _actions.Touch.PrimaryPosition.ReadValue<Vector2>();
        if(IsSwipe(_startTouchPos, endPos))
        {
            var swipeDir = GetSwipeDirection(endPos - _startTouchPos);
            OnMove?.Invoke(swipeDir);
        }
    }

    private bool IsSwipe(Vector2 start, Vector2 end)
    {
        if(Vector2.Distance(start, end) > _swipeLength) return true;
        return false;
    }

    private Vector2 GetSwipeDirection(Vector2 delta)
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
