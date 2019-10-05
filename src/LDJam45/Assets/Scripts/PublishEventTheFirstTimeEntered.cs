using UnityEngine;

public class PublishEventTheFirstTimeEntered : MonoBehaviour
{
    [SerializeField] private GameEvent Event;

    private bool _isPublished = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPublished)
            return;
        Event.Publish();
        _isPublished = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isPublished)
            return;
        Event.Publish();
        _isPublished = true;
    }
}