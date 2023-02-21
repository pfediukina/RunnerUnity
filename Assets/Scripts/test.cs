using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class test : MonoBehaviour
{
    void Awake()
    {
        Test();
    }

    public void Test()
    {
        // FirebaseDatabase.DefaultInstance.RootReference.GetValueAsync().ContinueWith( task =>
        // {
        //     if(task.IsCompleted)
        //     {
        //         DataSnapshot snapshot = task.Result;
        //         Debug.Log(snapshot.Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).GetRawJsonValue());

        //         // PlayerData.Name = (string)snapshot.Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("Name").Value;
        //         // PlayerData.Record = (int)snapshot.Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("Record").Value;
        //     }
        // });
    }
}