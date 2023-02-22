using System.Collections.Generic;
using System;
using Firebase.Auth;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class Auth : MonoBehaviour
{
    public static Action OnLoginSucess;

    [SerializeField] private AuthenticationUI _authUI;
    
    public static Dictionary<AuthError, string> AuthErrors = 
    new Dictionary<AuthError, string>()
    {
        {AuthError.WrongPassword, "Wrong password"},
        {AuthError.UserNotFound, "Account does not exist"},
        {AuthError.InvalidEmail, "Invalid email"},
        {AuthError.WeakPassword, "Weak password"},
        {AuthError.EmailAlreadyInUse, "Email already in use"},
        {AuthError.MissingPassword, "Missing password"},
        {AuthError.MissingEmail, "Missing email"}

    };

    private void Start()
    {
        if(PlayerPrefs.GetInt("Auth") == 1)
        {
            SignIn(PlayerPrefs.GetString("Login"), PlayerPrefs.GetString("Password"));
        }
    }

    //register
    public void SignUp(string email, string pass, string name)
    {
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread( task =>
        {
            if(task.IsCompleted)
            {
                FirebaseUser user = task.Result;
                SaveLogin(email, pass);
                PlayerDatabase.SavePlayerData(name, 0, OnLoginSucess);
            }
            else
            {
                FirebaseException fbEx = task.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)fbEx.ErrorCode;
                _authUI.ShowErrorMessage(AuthErrors[errorCode]);
            }
        });
    }

    //login
    public void SignIn(string email, string pass)
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread( task =>
        {
            if(task.IsCompleted)
            {
                FirebaseUser user = task.Result;
                UserProfile profile = new UserProfile();
                user.UpdateUserProfileAsync(profile);
                SaveLogin(email, pass);
                PlayerDatabase.GetPlayerData(OnLoginSucess);
            }
            else
            {
                FirebaseException fbEx = task.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)fbEx.ErrorCode;
                _authUI.ShowErrorMessage(AuthErrors[errorCode]);
            }
        });
    }

    private void SaveLogin(string email, string pass)
    {
        PlayerPrefs.SetString("Login", email);
        PlayerPrefs.SetString("Password", pass);
        PlayerPrefs.SetInt("Auth", 1);
    }
}