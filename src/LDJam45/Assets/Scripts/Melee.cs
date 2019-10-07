using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private Role OwnedBy;
    [SerializeField] private AudioClip OnHitSound;

    public bool IsDeadly = true;

    private void OnCollisionStay(Collision other) => Trigger(other.gameObject.GetComponent<Health>());
    private void OnTriggerStay(Collider other) => Trigger(other.GetComponent<Health>());

    private void Trigger(Health health)
    {
        if (!IsDeadly)
            return;

        if (health == null || health.Role.Equals(OwnedBy))
            return;
        health.ApplyDamage();
        if (OnHitSound != null)
            AudioSource.PlayClipAtPoint(OnHitSound, Camera.current.transform.position);
    }
}
