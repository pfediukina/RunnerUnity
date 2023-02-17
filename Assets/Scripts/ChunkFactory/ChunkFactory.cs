using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ChunkFactory : BaseFactory<Chunk>
{
    public float speed { get => _speed;}

    [SerializeField] private float _speed;
    [SerializeField] private Transform _playerPos;

    private int countOfChunks = 4;

    private void Start()
    {
        factoryObjects = InitChunks(countOfChunks + 1);
        CreateChunks(countOfChunks);
    }

    private void FixedUpdate()
    {
        //wip
        foreach(var obj in pooledObjects)
        {
            obj.SetSpeed(_speed);
        }
    }

    private void CreateChunks(int count)
    {
        Vector3 pos = transform.position;

        for(int i = 0; i < count; i++)
        {
            var chunk = SetNewChunk(pos);
            pos += Vector3.right  * Prefab.GetLength();
        }
    }

    private Chunk SetNewChunk(Vector3 pos)
    {
        var chunk = factoryObjects.Get();
        pooledObjects.Add(chunk);
        
        chunk.transform.position = pos;

        chunk.SetEndPos(_playerPos.position + Vector3.left * Prefab.GetLength() / 2 + Vector3.left * 10);
        chunk.Move = true;

        chunk.OnChunkNearPlayer += OnChunkNearPlayer;
        return chunk;
    }

    public void OnChunkNearPlayer(Chunk chunk)
    {
        //CreateChunks(1);
        Vector3 pos = chunk.transform.position + Vector3.right * Prefab.GetLength() * countOfChunks;
        SetNewChunk(pos);
        factoryObjects.Release(chunk);
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
            maxSize: 6);

        return list;
    }
}