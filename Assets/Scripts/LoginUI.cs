using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginUI : MonoBehaviour
{
    public static Action<string, string> OnContinueClickedLog;

    [HideInInspector] public AuthorizationUI Authorization;

    [SerializeField] private Button _continueButton;

    [Header("InputFields")]
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;

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
        OnContinueClickedLog?.Invoke(_email.text, _password.text);
    }
}
