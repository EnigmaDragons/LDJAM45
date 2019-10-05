using UnityEngine;

public class WinGameOnEnter : MonoBehaviour
{
    [SerializeField] private Navigator navigator;

    private void OnCollisionEnter(Collision other) => Win();

    private void Win()
    {
        navigator.NavigateToVictoryScene();
    }
}
