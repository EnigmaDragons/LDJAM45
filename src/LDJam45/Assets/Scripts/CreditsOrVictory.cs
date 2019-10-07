using System.Collections.Generic;
using UnityEngine;

public class CreditsOrVictory : MonoBehaviour
{
    [SerializeField] private GameState State;
    [SerializeField] private List<GameObject> VictoryElements;
    [SerializeField] private List<GameObject> CreditsElements;

    private void Start()
    {
        VictoryElements.ForEach(x => x.SetActive(State.IsVictory));
        CreditsElements.ForEach(x => x.SetActive(!State.IsVictory));
    }
}
