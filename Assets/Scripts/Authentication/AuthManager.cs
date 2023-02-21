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

    private void OnEnable()
    {
        Auth.OnLoginSucess += GoToMainMenu;
    }

    private void OnDisable()
    {
        Auth.OnLoginSucess -= GoToMainMenu;
    }

    public static void GoToMainMenu()
    {
        _instance.StartCoroutine(_instance.GoToMenu());
    }
    
    private IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(1);
    }
}