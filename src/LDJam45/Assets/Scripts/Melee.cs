using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private Role OwnedBy;

    public bool IsDeadly = true;

    private void OnCollisionStay(Collision other)
    {
        if (!IsDeadly)
            return;

        var health = other.gameObject.GetComponent<Health>();
        if (health == null || health.Role.Equals(OwnedBy))
            return;
        health.ApplyDamage();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsDeadly)
            return;

        var health = other.GetComponent<Health>();
        if (health == null || health.Role.Equals(OwnedBy))
            return;
        health.ApplyDamage();
    }
}
