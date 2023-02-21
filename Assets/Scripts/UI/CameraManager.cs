using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _delay;

    private void Start()
    {
        StartCoroutine(ChangeCamera());
    }  

    private IEnumerator ChangeCamera()
    {
        yield return new WaitForSeconds(_delay);
        _camera.Priority = 12;
    } 
}