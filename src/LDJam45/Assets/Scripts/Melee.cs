using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private Role OwnedBy;
    [SerializeField] private AudioClip OnHitSound;

    private Camera _camera;

    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
    }

    public bool IsDeadly = true;

    private void OnCollisionStay(Collision other) => Trigger(other.gameObject.GetComponent<Health>());
    private void OnTriggerStay(Collider other) => Trigger(other.GetComponent<Health>());

    private void OnCollisionEnter(Collision other) => PlaySound(other.gameObject.GetComponent<Health>());
    private void OnTriggerEnter(Collider other) => PlaySound(other.GetComponent<Health>());

    private void Trigger(Health health)
    {
        if (!IsDeadly)
            return;

        if (health == null || health.Role.Equals(OwnedBy))
            return;
        health.ApplyDamage();
    }

    private void PlaySound(Health health) 
    {
        if (!IsDeadly)
            return;

        if (health == null || health.Role.Equals(OwnedBy))
            return;
        if (OnHitSound != null)
            AudioSupport.PlayClipAt(OnHitSound, _camera.transform.position);
    }
}
