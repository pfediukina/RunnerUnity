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
    }

    public static void GoToMainMenu()
    {
        _instance.StartCoroutine(_instance.GoToMenu());
    }

    void Start()
    {
        if(FirebaseManager.Auth.CurrentUser != null)
        {
            FirebaseManager.GetUserInfo();
            AuthManager.GoToMainMenu();
        }
    }

    private IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(1);
    }
}