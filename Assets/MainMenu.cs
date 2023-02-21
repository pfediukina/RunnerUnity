using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class MainMenu : MonoBehaviour
{
    public void ChangeScene(int sceneID)
    {   
        FirebaseAuth.DefaultInstance.SignOut();
        SceneManager.LoadScene(sceneID);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
