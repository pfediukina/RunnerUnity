using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ChunkFactory : BaseFactory<Chunk>
{
    [SerializeField] private int _count;
    [SerializeField] private Transform _playerPos;

    private Chunk lastChunk;

    private void Start()
    {
        factoryObjects = InitChunks(_count);
        CreateChunks(4);
    }

    private void CreateChunks(int count)
    {
        Vector3 pos;
        if(lastChunk == null)
            pos = transform.position;
        else
            pos = lastChunk.transform.position + Vector3.right * Prefab.GetLength();

        for(int i = 0; i < count; i++)
        {
            SetNewChunk(pos);
            pos += Vector3.right  * Prefab.GetLength();
        }
    }

    private void SetNewChunk(Vector3 pos)
    {
        var chunk = factoryObjects.Get();
        chunk.transform.position = pos;

        chunk.SetEndPos(_playerPos.position + Vector3.left * Prefab.GetLength() / 2 + Vector3.left * 10);
        chunk.Move = true;

        lastChunk = chunk;
        chunk.OnChunkNearPlayer += OnChunkNearPlayer;
    }

    public void OnChunkNearPlayer(Chunk chunk)
    {
        CreateChunks(1);
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