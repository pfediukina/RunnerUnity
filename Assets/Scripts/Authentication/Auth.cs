using System.Collections.Generic;
using System;
using Firebase.Auth;
using UnityEngine;

public class Auth : MonoBehaviour
{
    public static Action OnLoginSucess;
    
    private Dictionary<AuthError, string> _authErrors = 
    new Dictionary<AuthError, string>()
    {
        {AuthError.WrongPassword, "Wrong password"},
        {AuthError.UserNotFound, "Account does not exist"},
        {AuthError.InvalidEmail, "Invalid email"},
        {AuthError.MissingPassword, "Missing password"},
        {AuthError.MissingEmail, "Missing email"}, 

        {AuthError.WeakPassword, "Weak password"},
        {AuthError.EmailAlreadyInUse, "Email already in use"},
        {AuthError.InvalidEmail, "Invalid email"},
        {AuthError.MissingPassword, "Missing password"},
        {AuthError.MissingEmail, "Missing email"}

    };

    private void Start()
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            PlayerDatabase.GetPlayerData();
            OnLoginSucess?.Invoke();
        }
    }

    //register
    private void SignUp(string email, string pass, string name)
    {
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWith( task =>
        {
            if(task.IsCompleted)
            {
                FirebaseUser user = task.Result;
                PlayerDatabase.SavePlayerData(name, 0);
                OnLoginSucess?.Invoke();
            }
            else
            {
                Debug.LogError(task.Exception.Message);
            }
        });
    }

    //login
    private void SignIn(string email, string pass, string name)
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, pass).ContinueWith( task =>
        {
            if(task.IsCompleted)
            {
                FirebaseUser user = task.Result;
                UserProfile profile = new UserProfile();
                user.UpdateUserProfileAsync(profile);
                PlayerDatabase.GetPlayerData();
                OnLoginSucess?.Invoke();
            }
            else
            {
                Debug.LogError(task.Exception.Message);
            }
        });
    }
}