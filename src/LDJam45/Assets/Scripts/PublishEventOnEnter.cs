using UnityEngine;

public class PublishEventOnEnter : MonoBehaviour
{
    [SerializeField] private GameEvent Event;

    private void OnCollisionEnter(Collision collision)
    {
        Event.Publish();
    }

    private void OnTriggerEnter(Collider other)
    {
        Event.Publish();
    }
}
