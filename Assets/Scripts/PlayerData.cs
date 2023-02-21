using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static bool IsNull => _data == null ? true : false;
    public static string Name { get => _data._name; set => _data._name = value; }
    public static int Record { get => _data._record; set => _data._record = value; }

    [SerializeField] private static PlayerData _data;
    [SerializeField] private string _name;
    [SerializeField] private int _record;

    void Awake()
    {
        if(_data == null)
            _data = this;

        DontDestroyOnLoad(this);
    }

    public static void SetPlayerInfo(string name, int record)
    {
        _data._name = name;
        _data._record = record;
    }
}
