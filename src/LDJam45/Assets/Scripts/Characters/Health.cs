using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject OnDeathVfx;
    [SerializeField] private float IFrames;
    [SerializeField] private Collider Collider;
    [SerializeField] private GameEvent OnDeathEvent;
    [SerializeField] private GameState GameState;
    [SerializeField] private CharacterID ID;

    public Role Role;
    public int MaxHealth;
    public Action OnDamage { private get; set; } = () => { };
    public float SecondsOfInvincibility;
    public bool IsInvincible;

    private bool _isDead = false;

    private void Start()
    {
        GameState.HealthMap[ID.ID] = MaxHealth;
        GameState.IsInvincibleMap[ID.ID] = SecondsOfInvincibility > 0 || IsInvincible;
    }

    private void Update()
    {
        if (Role == Role.Friendly && Input.GetKey("o") && Input.GetKey("p") && (Input.GetKeyDown("o") || Input.GetKeyDown("p")))
            GameState.HealthMap[ID.ID] += 5;

        SecondsOfInvincibility = Mathf.Max(0, SecondsOfInvincibility - Time.deltaTime);
        GameState.IsInvincibleMap[ID.ID] = SecondsOfInvincibility > 0 || IsInvincible;
    }

    public void ApplyDamage()
    {
        if (_isDead || GameState.IsInvincibleMap[ID.ID])
            return;

        GameState.HealthMap[ID.ID]--;
        if (GameState.HealthMap[ID.ID] <= 0)
        {
            _isDead = true;
            Debug.Log($"{name} is destroyed");
            PlayExplosion();
            StartCoroutine(ResolveDestruction());
        }
        else
        {
            SecondsOfInvincibility = IFrames;
            OnDamage();
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
