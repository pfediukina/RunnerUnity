using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using System.Collections;

public class AuthManager : MonoBehaviour
{
    public static void GoToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}