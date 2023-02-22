using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _tapText;
    [SerializeField] private TMPro.TextMeshProUGUI _playerName;

    private void Start()
    {
        _tapText.transform.DOScale(Vector3.one * 1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    void OnEnable()
    {
        _playerName.text = PlayerData.Name;
    }

    public void LogOut()
    {   
        Debug.Log(":c");
        FirebaseAuth.DefaultInstance.SignOut();
        
        PlayerPrefs.DeleteKey("Login");
        PlayerPrefs.DeleteKey("Password");
        PlayerPrefs.SetInt("Auth", 0);
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
