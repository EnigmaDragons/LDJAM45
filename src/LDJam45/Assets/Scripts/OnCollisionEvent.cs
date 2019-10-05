using System.Collections;
using UnityEngine;

public class OnCollisionEvent : MonoBehaviour
{
    [SerializeField] private GameEvent onCollide;
    [SerializeField] private FloatReference cooldown = new FloatReference(1);

    private bool _isOnCooldown = false;

    private void OnCollisionEnter(Collision other) => Trigger();
    private void OnParticleCollision(GameObject other) => Trigger();

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
