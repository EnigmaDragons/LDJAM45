using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject OnDeathVfx;
    [SerializeField] private GameEvent OnHealthGained;
    [SerializeField] private GameEvent OnHealthLost;
    [SerializeField] private GameEvent OnPlayerDashing;
    [SerializeField] private GameEvent OnPlayerStopDashing;
    [SerializeField] private float IFrames;
    [SerializeField] private Collider Collider;
    [SerializeField] private GameEvent OnDeathEvent;

    public bool JustGotHit { get; private set; } = false;
    private bool _isDashing = false;
    private bool _isDead = false;
    private float _secondsLeftOfInvincibility;

    public Role Role;
    public int MaxHealth;
    public int CurrentHealth { get; set; }
    public bool IsInvincible => _isDashing || JustGotHit;
    public Action OnDamage { private get; set; } = () => {};

    private void Start()
    {
        CurrentHealth = MaxHealth;
        if (Role == Role.Friendly)
        {
            OnPlayerDashing.Subscribe(() => _isDashing = true, this);
            OnPlayerStopDashing.Subscribe(() => _isDashing = false, this);
        }
    }

    private void OnDisable()
    {
        OnPlayerDashing.Unsubscribe(this);
        OnPlayerStopDashing.Unsubscribe(this);
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
