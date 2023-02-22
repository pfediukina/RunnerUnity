using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private DeathScreen _deathScreen;
    [SerializeField] private TMPro.TextMeshProUGUI _scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI _name;

    public void Start()
    {
        _name.text = PlayerData.Name;
    }

    void Update()
    {
        UpdateScore();
    }

    public void ShowDeathScreen()
    {
        _deathScreen.Show();
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
}
