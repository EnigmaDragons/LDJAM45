using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunTimePresenter : MonoBehaviour
{
    [SerializeField] private GameState GameState;
    [SerializeField] private TextMeshProUGUI RunTimer;

    private void Update()
    {
        if (GameState.PlayIronmanMode)
            RunTimer.text = TimeSpan.FromSeconds(GameState.RunTime).ToString(@"mm\:ss\.fff");
    }
}
