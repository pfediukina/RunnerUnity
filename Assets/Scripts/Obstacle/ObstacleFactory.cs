using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : BaseFactory<Obstacle>
{
    private string[] _lineStrings;
    private List<Obstacle> _objects = new List<Obstacle>();

    private GameObject _parent;

    private void Awake()
    {
        _lineStrings = new string[GameManager.GameSettings.NumberOfLines];
        
        _parent = new GameObject("Obs");
        _parent.transform.parent = transform;
        factoryObjects = InitPool(GetAmountOfObsPerLine() * GameManager.GameSettings.NumberOfLines, _parent.transform);
    }

    public void SpawnObstacles(Chunk chunk)
    {
        DespawnObstackles();
        GenerateObstacleStrings();
        for(int i = 0; i < GameManager.GameSettings.NumberOfLines; i++)
        {
            SpawnLine(i);
        }
    }

    private void SpawnLine(int line)
    {
        float lineXOffset = (line - GameManager.GameSettings.StartLine) * GameManager.GameSettings.LineDistance;

        for(int i = 0; i < _lineStrings[line].Length; i++)
        {
            if(_lineStrings[line][i] == '0') continue;
            else 
            {
                Vector3 pos = transform.position + Vector3.forward * i * GameManager.GameSettings.Distance;
                pos += Vector3.right * lineXOffset;

                if(_lineStrings[line][i] == '1')
                {
                    _objects.Add(SpawnObstacle(pos, true));
                }
                else
                {
                    _objects.Add(SpawnObstacle(pos, false));
                }
            }
        }
    }
    
    private Obstacle SpawnObstacle(Vector3 pos, bool isShort)
    {
        Obstacle obs = factoryObjects.Get();
        obs.transform.position = pos;

        if(isShort)
            obs.SetShortObstacle();
        else
            obs.SetLongObstacle();

        return obs;
    }

    private void DespawnObstackles()
    {
        foreach(var obj in _objects)
        {
            factoryObjects.Release(obj);
        }
    }

    private void GenerateObstacleStrings()
    {
        for(int i = 0; i < GameManager.GameSettings.NumberOfLines; i++)
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
        return (int)(Chunk.Lenght / GameManager.GameSettings.Distance);
    }
}