using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class FirebaseManager : MonoBehaviour
{
    public static DependencyStatus DependencyStatus;
    public static FirebaseAuth Auth;
    public static DatabaseReference Database;

    void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            DependencyStatus = task.Result;
            if(DependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
        });
    }

    public void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        Auth = FirebaseAuth.DefaultInstance;
        Database = FirebaseDatabase.DefaultInstance.RootReference;
    }
}