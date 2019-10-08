using System;
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
        Runtime.text = "Run Time: " + timespan.ToString(@"h\:mm\:ss\.fff");
    }
}
