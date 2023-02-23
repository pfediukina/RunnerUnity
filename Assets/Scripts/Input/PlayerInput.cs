using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInput : MonoBehaviour
{
    public static Action<Vector2> OnInput;

    private static PlayerActions _actions;
    private List<IPlayerInput> _inputs = new List<IPlayerInput>();

    private void Awake()
    {
        if(_actions == null)
            _actions = new PlayerActions();
    }

    private void OnEnable()
    {
        //_actions.Enable();
        CameraManager.OnCameraMoved += EnableInput;
    }
    
    private void OnDisable()
    {
        _actions.Disable();
        CameraManager.OnCameraMoved -= EnableInput;
    }

    private void Start()
    {
        InitControls();
    }

    public static void EnableInput()
    {
        _actions.Enable();
    }

    public static void DisableInput()
    {
        _actions.Disable();
    }

    private void InitControls()
    {
        _inputs.Add(new TouchInput(_actions));
        _inputs.Add(new KeyboardInput(_actions));

        foreach (IPlayerInput input in _inputs)
        {   
            input.OnDirectionInput += Input_OnDirectionInput;
        }
    }

    private void Input_OnDirectionInput(Vector2 direction)
    {
        OnInput?.Invoke(direction);
    }
}
