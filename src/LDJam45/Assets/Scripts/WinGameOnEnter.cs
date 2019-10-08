using UnityEngine;

public class WinGameOnEnter : MonoBehaviour
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private GameState state;

    private void OnCollisionEnter(Collision other) => Win();

    private void Win()
    {
        state.Win();
        navigator.NavigateToVictoryScene();
    }
}
