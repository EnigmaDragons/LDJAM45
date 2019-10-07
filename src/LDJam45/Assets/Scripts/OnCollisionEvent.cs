using System.Collections;
using UnityEngine;

public class OnCollisionEvent : MonoBehaviour
{
    [SerializeField] private GameEvent onCollide;
    [SerializeField] private FloatReference cooldown = new FloatReference(1);
    [SerializeField] private bool TriggerOnly = false;

    private bool _isOnCooldown = false;

    private void OnCollisionEnter(Collision other)
    {
        if (!TriggerOnly)
            Trigger();
    }

    private void OnTriggerEnter(Collider other) => Trigger();
    private void OnParticleCollision(GameObject other)
    {
        if (!TriggerOnly)
            Trigger();
    }

    private void Trigger()
    {
        if (_isOnCooldown)
            return;
        
        _isOnCooldown = true;
        onCollide.Publish();
        StartCoroutine(DeactivateCooldown());
    }

    private IEnumerator DeactivateCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        _isOnCooldown = false;
    }
}
