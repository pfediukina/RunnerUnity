using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using System.Collections;

public class AuthManager : MonoBehaviour
{
    private static AuthManager _instance;

    void Awake()
    {
        if(_instance == null)
            _instance = this;
        Auth.OnLoginSucess += GoToMainMenu;
    }

    public void GoToMainMenu()
    {
        Debug.Log("Hello 2");
        SceneManager.LoadScene(1);
    }
}