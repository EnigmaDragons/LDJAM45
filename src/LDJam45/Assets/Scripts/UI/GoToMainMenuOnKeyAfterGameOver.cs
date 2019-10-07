using UnityEngine;

public class GoToMainMenuOnKeyAfterGameOver : MonoBehaviour
{
    [SerializeField] private GameEvent onGameOver;
    [SerializeField] private Navigator navigator;

    private bool _acceptsButtons;

    private void OnEnable()
    {
        onGameOver.Subscribe(() => _acceptsButtons = true, this);
    }

    private void OnDisable()
    {
        onGameOver.Unsubscribe(this);
    }

    private void Update()
    {
        if (_acceptsButtons && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape)))
            navigator.NavigateToMainMenu();
    }
}
