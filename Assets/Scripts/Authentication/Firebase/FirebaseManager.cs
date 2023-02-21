using System;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;

public class FirebaseManager : MonoBehaviour
{


    public static void SaveUserInfo(Action OnComplete)
    {
        if(PlayerData.IsNull) return;
        FirebaseDatabase.DefaultInstance.RootReference.Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId)
                                            .SetRawJsonValueAsync(JsonUtility.ToJson(PlayerData.Data)).ContinueWith(task =>
                                            {
                                                if(task.IsCompleted) OnComplete?.Invoke();
                                            });

        // var task1 = FirebaseDatabase.DefaultInstance.RootReference.Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).
        //                                 Child("Name").SetValueAsync(PlayerData.Name);
        // var task2 = FirebaseDatabase.DefaultInstance.RootReference.Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).
        //                                 Child("Record").SetValueAsync(PlayerData.Record);
    }

    public static void GetUserInfo(Action OnComplete)
    {
        FirebaseDatabase.DefaultInstance.RootReference.GetValueAsync().ContinueWith( task =>
        {
            if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                PlayerData.GetData(snapshot.Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).GetRawJsonValue());
                

                // PlayerData.Name = (string)snapshot.Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("Name").Value;
                // PlayerData.Record = (int)snapshot.Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("Record").Value;
                OnComplete?.Invoke();
            }
        });
    }
}