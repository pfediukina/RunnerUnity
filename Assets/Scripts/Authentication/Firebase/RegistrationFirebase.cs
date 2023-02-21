using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class RegistrationFirebase : MonoBehaviour
{
    [SerializeField] private AuthenticationUI _authUI;
    
    private FirebaseUser _user;

    private void OnEnable()
    {
        RegistrationUI.OnContinueClickedReg += RegisterUser;
    }

    private void OnDisable()
    {
        RegistrationUI.OnContinueClickedReg -= RegisterUser;
    }

    public void RegisterUser(string name, string email, string password)
    {
        StartCoroutine(RegistrationProcess(name, email, password));
    }

    public IEnumerator RegistrationProcess(string name, string email, string password)
    {
        var RegisterTask = FirebaseManager.Auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

        if(RegisterTask.Exception != null)
        {
            FirebaseException fbEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)fbEx.ErrorCode;
            string message = "Register failed";
            //Debug.Log(errorCode);
            switch (errorCode)
            {
                case AuthError.WeakPassword:
                    message = "Weak password";
                    break;
                case AuthError.EmailAlreadyInUse:
                    message = "Email already in use";
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
            _user = RegisterTask.Result;
            if(_user  != null)
            {
                UserProfile profile = new UserProfile { DisplayName = name };
                var ProfileTask = _user.UpdateUserProfileAsync(profile);
                yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);
                if(ProfileTask.Exception != null)
                {
                    FirebaseException fbEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError)fbEx.ErrorCode;
                    Debug.LogWarning(ProfileTask.Exception.Message + $" Code: {errorCode}");
                }
                else
                {
                    PlayerData.Name = name;
                    PlayerData.Record = 0;
                    StartCoroutine(FirebaseManager.SaveUser());
                    AuthManager.GoToMainMenu();
                }
            }
        }
    }
}
