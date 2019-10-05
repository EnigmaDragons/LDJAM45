using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private Role OwnedBy;

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();
        if (health == null || health.Role.Equals(OwnedBy))
            return;
        health.ApplyDamage();
    }
}
