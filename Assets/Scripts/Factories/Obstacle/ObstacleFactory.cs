using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : BaseFactory<Obstacle>
{
    private string[] _lineStrings;
    private List<Obstacle> _objects = new List<Obstacle>();

    private GameObject _parent;

    private void Awake()
    {
        _lineStrings = new string[GameData.GameSettings.NumberOfLines];
        
        _parent = new GameObject("Obs");
        _parent.transform.parent = transform;

        factoryObjects = InitPool(GetAmountOfObsPerLine() * GameData.GameSettings.NumberOfLines, _parent.transform);
    }

    public void SpawnObstacles(Chunk chunk)
    {
        DespawnObstackles();
        GenerateObstacleStrings();
        for(int i = 0; i < GameData.GameSettings.NumberOfLines; i++)
        {
            SpawnLine(i);
        }
    }

    private void SpawnLine(int line)
    {
        float lineXOffset = (line - GameData.GameSettings.StartLine) * GameData.GameSettings.LineDistance;

        for(int i = 0; i < _lineStrings[line].Length; i++)
        {
            if(_lineStrings[line][i] == '0') continue;
            Vector3 pos = transform.position + Vector3.forward * i * GameData.GameSettings.Distance;
            pos += Vector3.right * lineXOffset;
            _objects.Add(SpawnObstacle(pos, _lineStrings[line][i] == '1' ? true : false));
        }
    }
    
    private Obstacle SpawnObstacle(Vector3 pos, bool isShort)
    {
        Obstacle obs = factoryObjects.Get();
        obs.transform.position = pos;
        obs.SetObstacle(isShort);
        return obs;
    }

    private void DespawnObstackles()
    {
        foreach(var obj in _objects)
        {
            factoryObjects.Release(obj);
        }
        _objects.Clear();
    }

    private void GenerateObstacleStrings()
    {
        for(int i = 0; i < GameData.GameSettings.NumberOfLines; i++)
        {
            _lineStrings[i] = GenerateString();
        }
    }

    private string GenerateString()
    {
        string _LineString = "";
        for(int i = 0; i < GetAmountOfObsPerLine(); i++)
        {
            _LineString += Mathf.Clamp(Random.Range(-1, 3), 0, 3).ToString();
        }
        return _LineString;
    }

    private int GetAmountOfObsPerLine()
    {
        return (int)(Chunk.Lenght / GameData.GameSettings.Distance);
    }
}