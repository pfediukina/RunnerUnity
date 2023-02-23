using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    
    [SerializeField] private float _inputDelay;
    [SerializeField] private float _delay;

    public static Action OnCameraMoved;

    void Awake()
    {
        OnCameraMoved = null;
    }

    private void Start()
    {
        StartCoroutine(ChangeCamera());
        StartCoroutine(CameraMoved());
    }  

    private IEnumerator ChangeCamera()
    {
        yield return new WaitForSeconds(_delay);
        _camera.Priority = 12;
    }

    private IEnumerator CameraMoved()
    {
        yield return new WaitForSeconds(_inputDelay + _delay);
        OnCameraMoved?.Invoke();
    } 
    
}