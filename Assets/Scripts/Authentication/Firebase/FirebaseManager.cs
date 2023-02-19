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

    private void Start()
    {
       CheckUser();
    }


    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        Auth = FirebaseAuth.DefaultInstance;
        Database = FirebaseDatabase.DefaultInstance.RootReference;
    }

     private void CheckUser()
     {
            if(Auth.CurrentUser != null)
                AuthManager.GoToMainMenu();
    }
}