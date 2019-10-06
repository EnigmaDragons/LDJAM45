using UnityEngine;

public class SetupGameState : MonoBehaviour
{
    [SerializeField] private GameState state;

    private void Awake()
    {
        state.Reset();
    }
}
