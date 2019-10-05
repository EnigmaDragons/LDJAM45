using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject OnDeathVfx;
    [SerializeField] private GameEvent OnHealthGained;
    [SerializeField] private GameEvent OnHealthLost;

    private bool _isDead;
    private float _secondsLeftOfInvincibility;

    public Role Role;
    public int MaxHealth;
    public int CurrentHealth { get; set; }
    public bool IsInvincible { get; set; } = false;
    public Action OnDamage { private get; set; } = () => {};

    private void Start()
    {
        CurrentHealth = MaxHealth;
        IsInvincible = false;
    }

    private void Update()
    {
        if (IsInvincible)
        {
            _secondsLeftOfInvincibility -= Time.deltaTime;
            if (_secondsLeftOfInvincibility <= 0)
                IsInvincible = false;
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
            IsInvincible = true;
            _secondsLeftOfInvincibility = 3f;
            OnDamage();
            if (Role == Role.Friendly)
                OnHealthLost.Publish();
        }
    }

    private void PlayExplosion()
    {
        var explosion = Instantiate(OnDeathVfx, transform.position, transform.rotation);
        explosion.transform.localScale = GetComponent<Renderer>().bounds.size;
        var explosionRigidBody = explosion.GetComponent<Rigidbody>();
        var rigidBody = GetComponent<Rigidbody>();
        if (rigidBody != null && explosionRigidBody != null)
            explosionRigidBody.velocity = rigidBody.velocity;
    }

    private IEnumerator ResolveDestruction()
    {
        yield return new WaitForSeconds(0.3f);
        if (Role.Equals(Role.Friendly))
        //do a game over here
        {}
        Destroy(gameObject);
    }
}
