using System;
using System.Collections;
using UnityEngine;

public static class PlayerDatabase
{
    public static void SavePlayerData(string name, int record, Action onComplete)
    {
        JsonData d = new JsonData();
        d.Name = name;
        d.Record  = record;
        string json = JsonUtility.ToJson(d);
        PlayerData.SetData(json);
        
        FirebaseManager.SaveUserInfo(onComplete, json);
    }

    public static void GetPlayerData(Action onComplete)
    {
        FirebaseManager.GetUserInfo(onComplete);
    }

    public static void SavePlayerRecord(int record)
    {
        JsonData d = new JsonData();
        PlayerData.Record = record;
        d.Name = PlayerData.Name;
        d.Record  = PlayerData.Record;
        string json = JsonUtility.ToJson(d);
        PlayerData.SetData(json);
        FirebaseManager.SaveUserInfo(null, json);
    }
}