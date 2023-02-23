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
            LoadingScreen.ShowWindow(true);
            SignIn(PlayerPrefs.GetString("Login"), PlayerPrefs.GetString("Password"));
        }
    }

    //register
    public void SignUp(string email, string pass, string name)
    {
        LoadingScreen.ShowWindow(true);
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread( task =>
        {
            if(task.IsCompleted)
            {
                
                if(task.Exception == null)
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
                    LoadingScreen.ShowWindow(false);
                }
            }
        });
    }

    //login
    public void SignIn(string email, string pass)
    {
        LoadingScreen.ShowWindow(true);
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread( task =>
        {
            if(task.IsCompleted)
            {
                if(task.Exception == null)
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
                    LoadingScreen.ShowWindow(false);
                    
                }
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