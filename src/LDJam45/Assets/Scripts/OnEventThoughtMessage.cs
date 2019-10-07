
using UnityEngine;

public class OnEventThoughtMessage : MonoBehaviour
{
    [SerializeField] private GameState state;
    [SerializeField] private GameEvent onEvent;
    [SerializeField] private string Message;
    [SerializeField, ReadOnly] private bool isFinished;

    private void OnEnable()
    {
        onEvent.Subscribe(QueueThought, this);
    }

    private void OnDisable()
    {
        onEvent.Unsubscribe(this);
    }

    private void QueueThought()
    {
        state.ThoughtsMessageQueue.Enqueue(Message);
        isFinished = true;
        onEvent.Unsubscribe(this);
    }
}
