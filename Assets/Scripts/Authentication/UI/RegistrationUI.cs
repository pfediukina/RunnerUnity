using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegistrationUI : MonoBehaviour
{
    public static Action<string, string, string> OnContinueClickedReg;

    [HideInInspector] public AuthenticationUI Authorization;
    
    [SerializeField] private Button _continueButton;

    [Header("InputFields")]
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _password1;
    [SerializeField] private TMP_InputField _password2;

    void OnEnable()
    {
        _continueButton.onClick.AddListener(ContinueClicked);
    }

    void OnDisable()
    {
        _continueButton.onClick.RemoveListener(ContinueClicked);
    }

    public void ContinueClicked()
    {
        if(CheckInput())
        {
            OnContinueClickedReg?.Invoke(_name.text, _email.text, _password1.text);
        }
    }

    public bool CheckInput()
    {
        if(_name.text == "")
        {
            Authorization.ShowErrorMessage("Missing name!");
            return false;
        }

        if(_password1.text != _password2.text)
        {
            Authorization.ShowErrorMessage("Password mismatch!");
            return false;
        }
        return true;
    }
}
