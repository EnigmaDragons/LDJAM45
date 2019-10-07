using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private Role OwnedBy;
    [SerializeField] private AudioClip OnHitSound;

    private void OnCollisionEnter(Collision other) => Trigger(other.gameObject.GetComponent<Health>());
    private void OnTriggerEnter(Collider other) => Trigger(other.GetComponent<Health>());

    private void Trigger(Health health)
    {
        if (health == null || health.Role.Equals(OwnedBy))
            return;
        health.ApplyDamage();
        if (OnHitSound != null)
            AudioSource.PlayClipAtPoint(OnHitSound, Camera.current.transform.position);
    }
}
