using System;
using System.Collections;
using UnityEngine;

public class CatDash : MonoBehaviour
{
    public Action OnFinished = () => {};

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
    }

    void Update()
    {
        DashCooldownRemaining = Mathf.Max(0, DashCooldownRemaining - Time.deltaTime);
        if (!_isDashing)
            return;

        if (_direction != Vector3.zero)
            transform.forward = _direction;
        var dashVelocity = Vector3.Scale(transform.forward, DashSpeed * new Vector3(
            (Mathf.Log(1f / (Time.deltaTime * _catBody.drag + 1)) / -Time.deltaTime),
            0,
            (Mathf.Log(1f / (Time.deltaTime * _catBody.drag + 1)) / -Time.deltaTime)));
        _catBody.AddForce(dashVelocity, ForceMode.VelocityChange);

        _dashTime -= Time.deltaTime;
        if (_dashTime <= 0)
        {
            _isDashing = false;
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
