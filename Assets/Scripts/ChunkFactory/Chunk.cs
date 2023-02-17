using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [HideInInspector] public bool Move = false;
    [SerializeField] MeshRenderer _meshRenderer;
    
    public Action<Chunk> OnChunkNearPlayer;

    private Vector3 _endPos;
    private float _speed = 1;

    public void Update()
    {
        if(Move)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPos, Time.deltaTime * _speed);
            if(Vector3.Distance(transform.position, _endPos) < 0.1f)
            {
                OnChunkNearPlayer.Invoke(this);
                Move = false;
            }
        }
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetEndPos(Vector3 pos)
    {
        _endPos = pos;
        _endPos.y = 0;
    }

    public float GetWidth()
    {
        return _meshRenderer.bounds.size.z;
    }

    public float GetLength()
    {
        return _meshRenderer.bounds.size.x;
    }
}
