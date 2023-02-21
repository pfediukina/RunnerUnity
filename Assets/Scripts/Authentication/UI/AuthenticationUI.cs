using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AuthenticationUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _errorText;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _buttonText;

    [SerializeField] private RegistrationUI _registerUI;
    [SerializeField] private LoginUI _loginUI;
    private bool _isLogin = false;

    void Awake()
    {
        ShowCurrentWindow();
    }

    public void HideAllWindows()
    {
        ShowLogin(false);
        ShowRegistration(!false);
    }

    public void ShowErrorMessage(string error)
    {
        _errorText.gameObject.SetActive(true);
        _errorText.text = error;
        ShowCurrentWindow();
    }

    public void HideErrorText()
    {
        _errorText.text = "";
        _errorText.gameObject.SetActive(false);
    }

    public void SwapWindow()
    {
        HideErrorText();
        _isLogin = !_isLogin;
        ShowCurrentWindow();
    }

    private void ShowLogin(bool show)
    {
        _loginUI.gameObject.SetActive(show);
        if(show)
        {
            _titleText.text = "LOGIN";
            _buttonText.text = "Sign up";
        }
    }

    private void ShowRegistration(bool show)
    {
        _registerUI.gameObject.SetActive(show);
        if(show)
        {
            _titleText.text = "REGISTRATION";
            _buttonText.text = "Sign in";
        }
    }

    private void ShowCurrentWindow()
    {
        ShowLogin(_isLogin);
        ShowRegistration(!_isLogin);
    }
}