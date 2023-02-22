using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegistrationUI : MonoBehaviour
{    
    [SerializeField] private Button _continueButton;

    [Header("InputFields")]
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _password1;
    [SerializeField] private TMP_InputField _password2;

    public InputData GetValues()
    {
        InputData data = new InputData();
        var error = CheckInput();
        if(error == "")
        {
            data.HasError = false;
            data.Email = _email.text;
            data.Name = _name.text;
            data.Password = _password1.text;
        }
        else
        {   
            data.HasError = true;
            data.AuthError = error;
        }
        return data;
    }

    public string CheckInput()
    {
        if(_name.text == "")
        {
            return "Missing name!";
        }

        if(_password1.text != _password2.text)
        {
            return "Password mismatch!";
        }
        return "";
    }
}