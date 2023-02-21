using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseAuth Auth;
    public static DatabaseReference Database;

    private void Awake()
    {
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        Auth = FirebaseAuth.DefaultInstance;
        Database = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public static IEnumerator SaveUser()
    {
        if(PlayerData.IsNull) yield break;

        var task1 = FirebaseDatabase.DefaultInstance.RootReference.Child(Auth.CurrentUser.UserId).Child("Name").SetValueAsync(PlayerData.Name);
        var task2 = FirebaseDatabase.DefaultInstance.RootReference.Child(Auth.CurrentUser.UserId).Child("Record").SetValueAsync(PlayerData.Record);
        yield return new WaitUntil(predicate: () => task1.IsCompleted && task2.IsCompleted);
    }

    public static void GetUserInfo()
    {
        FirebaseDatabase.DefaultInstance.RootReference.GetValueAsync().ContinueWith( task =>
        {
            if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                PlayerData.Name = (string)snapshot.Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("Name").Value;
                PlayerData.Record = (int)snapshot.Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("Record").Value;
            }
        });
    }
}