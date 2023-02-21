using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private DeathScreen _deathScreen;

    public void ShowDeathScreen()
    {
        _deathScreen.Show();
    }
}
