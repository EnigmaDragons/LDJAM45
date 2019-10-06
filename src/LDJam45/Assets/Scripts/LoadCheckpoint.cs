using UnityEngine;

public class LoadCheckpoint : MonoBehaviour
{
    [SerializeField] private GameEvent CheckpointLoaded;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameState GameState;

    private void OnEnable() => CheckpointLoaded.Subscribe(() =>
    {

        Player.transform.position = GameState.LastCheckpoint;
    }, this);
    private void OnDisable() => CheckpointLoaded.Unsubscribe(this);
}
