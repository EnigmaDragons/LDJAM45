using UnityEngine;

public class GoToMainMenuOnKey : MonoBehaviour
{
    [SerializeField] private Navigator navigator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            navigator.NavigateToMainMenu();
    }
}
