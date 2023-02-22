using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private DeathScreen _deathScreen;
    [SerializeField] private CanvasGroup _loadingScreen;
    [SerializeField] private Advertising _adsScreen;

    [SerializeField] private TMPro.TextMeshProUGUI _scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI _countText;
    [SerializeField] private TMPro.TextMeshProUGUI _name;

    public void Start()
    {
        _name.text = PlayerData.Name;
        _countText.text = "";
    }

    void OnEnable()
    {
        Advertising.OnPlayerSawAd += AfterAd;
    }

    void Update()
    {
        UpdateScore();
    }

    private void AfterAd()
    {
        ShowLoadingScreen(false);
        ShowDeathScreen(false);
        StartCoroutine(Count(3));
    }

    private IEnumerator Count(int counter)
    {
        for(int i = counter; i >= 1; i--)
        {
            _countText.text = i.ToString();
            yield return new WaitForSeconds(1);
            if(i == 1)
            { 
                _countText.text = "";
                GameLifetime.ResumeGame();
            }
        }
    }

    public void ShowLoadingScreen(bool show)
    {
        ShowDeathScreen(false);
        if(_loadingScreen == null) return;
        _loadingScreen.alpha = show ? 1 : 0;
        _loadingScreen.blocksRaycasts = show ? true : false;
    }

    public void ShowDeathScreen(bool show)
    {
        if(show) 
        _deathScreen.Show();
        else _deathScreen.Hide();
    }

    public void UpdateScore()
    {
        //Debug.Log(GameManager.Score);
        _scoreText.text = ((int)GameLifetime.Score).ToString();
    }

    private IEnumerator SetPlayerName()
    {
        yield return new WaitForSeconds(0.1f);
        _name.text = PlayerData.Name;
    }

    private void SetCount(int num)
    {
        if(num == -1)
        {
            _countText.text = "";
        }
    }
}
