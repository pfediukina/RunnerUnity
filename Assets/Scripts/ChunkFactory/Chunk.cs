using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] MeshRenderer _meshRenderer;
    
    public Action<Chunk> OnChunkBehindPlayer;

    private Vector3 _endPos;

    private void Update()
    {
        MoveChunk();
    }

    public void MoveChunk()
    {
        transform.position = Vector3.MoveTowards(transform.position, _endPos, Time.deltaTime * GameManager.Speed);
        if(Vector3.Distance(transform.position, _endPos) == 0)
        {
            OnChunkBehindPlayer?.Invoke(this);
        }
    }

    public void SetChunk(Vector3 startPos, Vector3 endPos)
    {
        transform.position = startPos;
        _endPos = endPos;
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
