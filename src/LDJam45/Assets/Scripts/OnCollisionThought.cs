using UnityEngine;

public class OnCollisionThought : MonoBehaviour
{
    [SerializeField] private GameState state;
    [SerializeField] private string message;
    [SerializeField, ReadOnly] private bool isFinished;

    private void OnCollisionEnter(Collision other) => Trigger(other.gameObject);
    private void OnTriggerEnter(Collider other) => Trigger(other.gameObject);
    private void OnParticleCollision(GameObject other) => Trigger(other.gameObject);

    private void Trigger(GameObject other)
    {
        if (isFinished || !other.tag.Equals("Player"))
            return;

        isFinished = true;
        state.ThoughtsMessageQueue.Enqueue(message);
    }
}
