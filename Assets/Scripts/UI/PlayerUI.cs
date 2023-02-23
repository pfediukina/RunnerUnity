using System.Collections;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private DeathScreen _deathScreen;
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
                PlayerInput.EnableInput();
            }
        }
    }

    public void ShowLoadingScreen(bool show)
    {
        PlayerInput.DisableInput();
        LoadingScreen.ShowWindow(show);
    }

    public void ShowDeathScreen(bool show)
    {
        PlayerInput.DisableInput();
        if(show) 
        _deathScreen.Show();
        else _deathScreen.Hide();
    }

    public void UpdateScore()
    {
        //Debug.Log(GameManager.Score);
        _scoreText.text = ((int)GameLifetime.Score).ToString();
    }
}
