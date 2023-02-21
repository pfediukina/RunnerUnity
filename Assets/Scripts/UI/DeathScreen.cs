using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _gameOver;
    [SerializeField] private DoTweenText _gameOverText;
    [SerializeField] private Button _mainMenu;

    private float _interactableTimer = 1;

    public void Awake()
    {
        _gameOverText.OnCompleteTask += OnAnimatedTextComplited;
    }
    public void Show()
    {
        _gameOver.alpha = 1;
        _gameOverText.AnimateText();
    }

    private void OnAnimatedTextComplited()
    {
        _mainMenu.gameObject.SetActive(true);
        StartCoroutine(SetInteractableDeathButton());
    }

    private IEnumerator SetInteractableDeathButton()
    {
        yield return new WaitForSeconds(_interactableTimer);
        _gameOver.interactable = true;
    } 

    private void OnPlayerClickedButton()
    {

    }
}
