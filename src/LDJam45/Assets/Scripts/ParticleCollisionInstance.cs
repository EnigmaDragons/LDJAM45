using UnityEngine;
using System.Collections.Generic;
using System;

public class ParticleCollisionInstance : MonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public float Offset = 0;
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;
    public bool UseFirePointRotation;

    public int Damage { private get; set; } = 10;
    public Role OwnedBy { private get; set; } = Role.All;


    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private bool isDestroying;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        Destroy(gameObject, 99f);
    }

    void OnParticleCollision(GameObject other)
    {
        var health = other.GetComponent<Health>();
        Debug.Log($"Particle Collided with {other.name}");
        if (health != null && health.Role.Equals(OwnedBy))
            return;
        Debug.Log($"Particle Collided with {other.name}");
        health.CurrentHealth -= Damage;

        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            foreach (var effect in EffectsOnCollision)
            {
                var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
                if (UseFirePointRotation) { instance.transform.LookAt(transform.position); }
                else { instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal); }
                if (!UseWorldSpacePosition) instance.transform.parent = transform;
                Destroy(instance, DestroyTimeDelay);
            }
        }
        Destroy();
    }

    private void Destroy()
    {
        if (isDestroying)
            return;

        isDestroying = true;
        Destroy(gameObject, DestroyTimeDelay + 0.5f);
    }
}
