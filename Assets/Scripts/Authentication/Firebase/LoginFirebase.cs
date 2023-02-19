using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;

public class LoginFirebase : MonoBehaviour
{
    [SerializeField] private AuthenticationUI _authUI;
    private FirebaseUser _user;


    private void OnEnable()
    {
        LoginUI.OnContinueClickedLog += LoginUser;
    }

    private void OnDisable()
    {
        LoginUI.OnContinueClickedLog -= LoginUser;
    }


    public void LoginUser(string email, string password)
    {
        
        //Debug.Log("Login");
        StartCoroutine(LoginProcess(email, password));
    }

    public IEnumerator LoginProcess(string email, string password)
    {
        var LoginTask = FirebaseManager.Auth.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if(LoginTask.Exception != null)
        {
            FirebaseException fbEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)fbEx.ErrorCode;
            string message = "Login failed";
            //Debug.Log(errorCode);
            switch (errorCode)
            {
                case AuthError.WrongPassword:
                    message = "Wrong password";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing password";
                    break;
                case AuthError.MissingEmail:
                    message = "Missing email";
                    break;
            }
            _authUI.ShowErrorMessage(message);
        }
        else
        {
            AuthManager.GoToMainMenu();
        }
        // else
        // {
        //     _user = RegisterTask.Result;
        //     if(_user  != null)
        //     {
        //         UserProfile profile = new UserProfile { DisplayName = name };
        //         var ProfileTask = _user.UpdateUserProfileAsync(profile);
        //         yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);
        //         if(ProfileTask.Exception != null)
        //         {
        //             FirebaseException fbEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
        //             AuthError errorCode = (AuthError)fbEx.ErrorCode;
        //             Debug.LogWarning(ProfileTask.Exception.Message + $" Code: {errorCode}");
        //         }
        //         else
        //         {
        //             Debug.Log("Registered");
        //         }
        //     }
        // }
    }
}
