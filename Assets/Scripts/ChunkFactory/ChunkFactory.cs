using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ChunkFactory : BaseFactory<Chunk>
{
    private Transform lastChunk;
    private GameObject _parent;

    private void Awake()
    {
        _parent = new GameObject("Chunks");
        _parent.transform.parent = transform;

        factoryObjects = InitPool(GameManager.GameSettings.NumberOfChunks, _parent.transform);
    }

    private void Start()
    {
        SpawnInitialChunks(GameManager.GameSettings.NumberOfChunks);
    }

    private void SpawnInitialChunks(int amount)
    {
        SpawnChunk(transform.position);

        for(int i = 1; i < amount; i++)
        {
            SpawnChunk(SpawnPosition());
        }
    }

    private Chunk SpawnChunk(Vector3 pos)
    {
        Chunk chunk = factoryObjects.Get();
        if(lastChunk != null) //if not first
        {   
            chunk.SetObstacles();
        }
        lastChunk = chunk.transform;
        
        chunk.OnChunkBehindPlayer -= OnChunkBehindPlayer;
        chunk.OnChunkBehindPlayer += OnChunkBehindPlayer;

        chunk.SetChunk(pos, GetEndPosition());
        chunk.MoveChunk();
        return chunk;
    }

    private void OnChunkBehindPlayer(Chunk chunk)
    {
        factoryObjects.Release(chunk);
        SpawnChunk(SpawnPosition());
    }

    private Vector3 SpawnPosition()
    {
        Vector3 position = lastChunk.position + Vector3.forward * Chunk.Lenght;
        return position;
    }

    private Vector3 GetEndPosition()
    {
        Vector3 position = transform.position + Vector3.back * Chunk.Lenght + Vector3.back * 5;
        return position;
    }
}