using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public struct InputData
{
    public bool HasError;
    public string AuthError;
    public string Email;
    public string Password;
    public string Name;
}

public class AuthenticationUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _errorText;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _buttonText;

    [SerializeField] private RegistrationUI _registerUI;
    [SerializeField] private LoginUI _loginUI;
    [SerializeField] private Auth _auth;

    private bool _isLogin = true;

    void Awake()
    {
        ShowCurrentWindow();
    }

    public void OnButtonContinueClicked()
    {
        HideErrorText();
        InputData data = _isLogin ? _loginUI.GetValues() : _registerUI.GetValues();
        if(data.HasError)
        {
            LoadingScreen.ShowWindow(false);
            ShowErrorMessage(data.AuthError);
            return;
        }
        if(_isLogin) _auth.SignIn(data.Email, data.Password);
        else _auth.SignUp(data.Email, data.Password, data.Name);
    }

    public void ShowErrorMessage(string error)
    {
        _errorText.gameObject.SetActive(true);
        _errorText.text = error;
        ShowCurrentWindow();
    }

    private void HideErrorText()
    {
        _errorText.text = "";
        _errorText.gameObject.SetActive(false);
    }

    private void SwapWindow()
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