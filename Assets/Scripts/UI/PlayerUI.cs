using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _gameOver;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private DoTweenText _gameOverText;

    private float _interactableTimer = 1;
    private int _score;

    public void Awake()
    {
        _gameOverText.OnCompleteTask += OnAnimatedTextComplited;
    }
    public void ShowDeathScreen(int score)
    {
        _score = score;
        _gameOver.alpha = 1;
        _gameOverText.AnimateText();
    }

    private void OnAnimatedTextComplited()
    {
        _scoreText.text = "SCORE: " + _score;
        StartCoroutine(SetInteractableDeathButton());
    }

    private IEnumerator SetInteractableDeathButton()
    {
        yield return new WaitForSeconds(_interactableTimer);
        _gameOver.interactable = true;
    } 
}
