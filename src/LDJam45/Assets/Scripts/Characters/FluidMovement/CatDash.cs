using System;
using System.Collections;
using UnityEngine;

public class CatDash : MonoBehaviour
{
    public Action OnFinished;

    [SerializeField] private float DashSpeed;
    [SerializeField] private float DashLength = 2;
    [SerializeField] private float PostEmitTime;
    [SerializeField] private Health Health;
    [SerializeField] private TrailRenderer DashTrail;
    [SerializeField] private float DashCooldown;

    private Rigidbody _catBody;

    private bool _isDashing;
    private float _dashTime;
    private Vector3 _direction;

    public float DashCooldownRemaining;

    private void Start()
    {
        _catBody = GetComponent<Rigidbody>();
    }

    public void Dash(Vector3 direction)
    {
        if (DashCooldownRemaining <= 0)
        {
            _isDashing = true;
            Health.IsDashing = true;
            _dashTime = DashLength;
            DashTrail.emitting = true;
            _direction = direction;
            DashCooldownRemaining = DashCooldown;
        }
        else
            OnFinished();
    }

    void Update()
    {
        DashCooldownRemaining = Mathf.Max(0, DashCooldownRemaining - Time.deltaTime);
        if (!_isDashing)
            return;

        if (_direction != Vector3.zero)
            transform.forward = _direction;
        _catBody.AddForce(_direction * DashSpeed * Time.fixedDeltaTime, ForceMode.Force);

        _dashTime -= Time.deltaTime;
        if (_dashTime <= 0)
        {
            _isDashing = false;
            Health.IsDashing = false;
            StartCoroutine(TurnOffEmissionsAfterDelay());
            OnFinished();
        }
    }

    private IEnumerator TurnOffEmissionsAfterDelay()
    {
        yield return new WaitForSeconds(PostEmitTime);
        if (!_isDashing)
            DashTrail.emitting = false;
    }
}
