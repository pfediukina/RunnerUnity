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
        if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            Debug.Log("Autologin");
            PlayerDatabase.GetPlayerData(OnLoginSucess);
        }
    }

    //register
    public void SignUp(string email, string pass, string name)
    {
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWith( task =>
        {
            if(task.IsCompleted)
            {
                FirebaseUser user = task.Result;
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
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, pass).ContinueWith( task =>
        {
            if(task.IsCompleted)
            {
                FirebaseUser user = task.Result;
                UserProfile profile = new UserProfile();
                user.UpdateUserProfileAsync(profile);
                PlayerDatabase.GetPlayerData(OnLoginSucess);
            }
            else
            {
                FirebaseException fbEx = task.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)fbEx.ErrorCode;
                _authUI.ShowErrorMessage(AuthErrors[errorCode]);                Debug.LogError(task.Exception.Message);
            }
        });
    }

    // private void SuccessLogin()
    // {
    //     OnLoginSucess?.Invoke();
    // }
}