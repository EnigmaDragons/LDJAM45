using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject OnDeathVfx;
    [SerializeField] private GameEvent OnHealthGained;
    [SerializeField] private GameEvent OnHealthLost;
    [SerializeField] private float IFrames;
    [SerializeField] private Collider Collider;
    [SerializeField] private GameEvent OnDeathEvent;

    public bool JustGotHit { get; private set; } = false;
    public bool IsDashing = false;
    private bool _isDead = false;
    private float _secondsLeftOfInvincibility;

    public Role Role;
    public int MaxHealth;
    public int CurrentHealth { get; set; }
    public bool IsInvincible => IsDashing || JustGotHit;
    public Action OnDamage { private get; set; } = () => {};

    private void Start()
    {
        CurrentHealth = MaxHealth;
        if (Role == Role.Friendly)
        {
        }
    }

    private void Update()
    {
        if (JustGotHit)
        {
            _secondsLeftOfInvincibility -= Time.deltaTime;
            if (_secondsLeftOfInvincibility <= 0)
                JustGotHit = false;
        }
    }

    public void ApplyDamage()
    {
        if (_isDead || IsInvincible)
            return;

        CurrentHealth -= 1;
        Debug.Log($"Health is now {CurrentHealth}");
        if (CurrentHealth <= 0)
        {
            _isDead = true;
            Debug.Log($"{name} is destroyed");
            PlayExplosion();
            StartCoroutine(ResolveDestruction());
        }
        else
        {
            JustGotHit = true;
            _secondsLeftOfInvincibility = IFrames;
            OnDamage();
            if (Role == Role.Friendly)
                OnHealthLost.Publish();
        }
    }

    private void PlayExplosion()
    {
        var explosion = Instantiate(OnDeathVfx, transform.position, transform.rotation);
        explosion.transform.localScale = Collider.bounds.size;
        var explosionRigidBody = explosion.GetComponent<Rigidbody>();
        var rigidBody = GetComponent<Rigidbody>();
        if (rigidBody != null && explosionRigidBody != null)
            explosionRigidBody.velocity = rigidBody.velocity;
    }

    private IEnumerator ResolveDestruction()
    {
        yield return new WaitForSeconds(0.3f);
        OnDeathEvent?.Publish();
        Destroy(gameObject);
    }
}
