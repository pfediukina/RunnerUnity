using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginUI : MonoBehaviour
{
    [Header("InputFields")]
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;

    public InputData GetValues()
    {
        InputData data = new InputData();
        data.HasError = false;
        data.Email = _email.text;
        data.Password = _password.text;
        return data;
    }
}
