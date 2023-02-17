using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [HideInInspector] public bool Move = false;

    [SerializeField] float _velocity = 1;
    [SerializeField] MeshRenderer _meshRenderer;
    
    public Action<Chunk> OnChunkNearPlayer;

    private Vector3 _endPos;


    public void Update()
    {
        if(Move)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPos, Time.deltaTime * _velocity);
            if(Vector3.Distance(transform.position, _endPos) < 0.1f)
            {
                OnChunkNearPlayer.Invoke(this);
                Move = false;
            }
        }
    }

    public void SetEndPos(Vector3 pos)
    {
        _endPos = pos;
        _endPos.y = 0;
    }

    public Vector2 GetSize()
    {
        return new Vector2(_meshRenderer.bounds.size.x, _meshRenderer.bounds.size.z);
    }
}
