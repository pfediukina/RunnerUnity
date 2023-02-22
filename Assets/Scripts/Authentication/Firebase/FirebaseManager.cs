using System;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{


    public static void SaveUserInfo(Action onComplete, string jsonData)
    {
        if(PlayerData.IsNull) return;
        FirebaseDatabase.DefaultInstance.RootReference.Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId)
                                            .SetRawJsonValueAsync(jsonData).ContinueWithOnMainThread(task =>
                                            {
                                                if(task.IsCompleted) onComplete?.Invoke();
                                            });                                
    }

    public static void GetUserInfo(Action OnComplete)
    {
        FirebaseDatabase.DefaultInstance.RootReference.GetValueAsync().ContinueWithOnMainThread( task =>
        {
            if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                PlayerData.SetData(snapshot.Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).GetRawJsonValue());
                
                OnComplete?.Invoke();
            }
        });
    }
}