using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _gameOver;
    [SerializeField] private DoTweenText _gameOverText;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Button _death;

    private float _interactableTimer = 1;

    public void Awake()
    {
        _gameOverText.OnCompleteTask += OnAnimatedTextComplited;
        _score.text = "";
    }
    public void Show()
    {
        _gameOver.alpha = 1;
        _gameOverText.AnimateText();
    }

    public void Hide()
    {
        _score.text = "";
        _gameOver.alpha = 0;
        _gameOver.interactable = false;
    }

    public void OnPlayerClickedMenu()
    {
        if(GameLifetime.Score > PlayerData.Record)
        {
            PlayerDatabase.SavePlayerRecord((int)GameLifetime.Score);
        }
        GameLifetime.ChangeScene(1);
    }

    private void SetPlayerScore()
    {
        _score.text = "SCORE: " + ((int)GameLifetime.Score).ToString();
        if(GameLifetime.Score > PlayerData.Record)
        {
            _score.text += "\nNEW RECORD!";
            PlayerDatabase.SavePlayerRecord((int)GameLifetime.Score);
        }
    }

    private void OnAnimatedTextComplited()
    {
        foreach(var b in _buttons)
            b.gameObject.SetActive(true);
        
        SetPlayerScore();
        StartCoroutine(SetInteractableDeathButton());
    }

    private IEnumerator SetInteractableDeathButton()
    {
        yield return new WaitForSeconds(_interactableTimer);
        _gameOver.interactable = true;
        if(GameData.IsWatchedAd) _death.interactable = false;
    } 
}
