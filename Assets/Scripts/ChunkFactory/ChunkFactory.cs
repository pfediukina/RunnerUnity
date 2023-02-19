using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ChunkFactory : BaseFactory<Chunk>
{
    [SerializeField] private Transform _playerPosition;

    Transform lastChunk;

    private void Awake()
    {
        factoryObjects = InitChunks(GameManager.NumberOfChunks);
    }

    private void Start()
    {
        SpawnInitialChunks(GameManager.NumberOfChunks);
    }

    private void SpawnInitialChunks(int amount)
    {
        CreateChunk(_playerPosition.position);
        for(int i = 1; i < amount; i++)
        {
            CreateChunk(SpawnPosition());
        }
    }

    private Chunk CreateChunk(Vector3 pos)
    {
        Chunk chunk = factoryObjects.Get();
        
        //проверка на подписанный ивент
        chunk.OnChunkBehindPlayer -= OnChunkBehindPlayer;
        chunk.OnChunkBehindPlayer += OnChunkBehindPlayer;

        chunk.SetChunk(pos, GetEndPosition());
        chunk.MoveChunk();

        lastChunk = chunk.transform;
        return chunk;
    }

    private void OnChunkBehindPlayer(Chunk chunk)
    {
        factoryObjects.Release(chunk);
        CreateChunk(SpawnPosition());
    }

    private Vector3 SpawnPosition()
    {
        Vector3 position = lastChunk.position + Vector3.right * Prefab.GetLength();
        return position;
    }

    private Vector3 GetEndPosition()
    {
        Vector3 position = transform.position + Vector3.left * Prefab.GetLength() + Vector3.left * 10;
        return position;
    }
    
    private ObjectPool<Chunk> InitChunks(int count)
    {
        GameObject chunks = new GameObject("Chunks");
        chunks.transform.position = transform.position;

        ObjectPool<Chunk> list = new ObjectPool<Chunk>(createFunc: () =>
            Instantiate(Prefab, chunks.transform),
            actionOnGet: (obj) => obj.gameObject.SetActive(true), 
            actionOnRelease: (obj) => obj.gameObject.SetActive(false), 
            actionOnDestroy: (obj) => Destroy(obj), 
            collectionCheck: false,
            maxSize: count);

        return list;
    }
}