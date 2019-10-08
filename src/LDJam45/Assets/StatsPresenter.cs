using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPresenter : MonoBehaviour
{
    [SerializeField] private GameState GameState;
    [SerializeField] private TextMeshProUGUI Runtime;
    [SerializeField] private TextMeshProUGUI DamageTaken;

    public void Start()
    {
        var timespan = TimeSpan.FromSeconds(GameState.RunTime);
        Runtime.text = timespan.ToString("c");
    }
}
