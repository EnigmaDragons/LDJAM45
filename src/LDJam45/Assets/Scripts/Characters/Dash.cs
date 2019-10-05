using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float DashLength = 2;
    [SerializeField] private float DashCooldown = 2;
    [SerializeField] private GameEvent PlayerActionStarted;
    [SerializeField] private GameEvent PlayerActionFinished;
    [SerializeField] private GameEvent DashingStarted;
    [SerializeField] private GameEvent DashingFinished;
    [SerializeField] private TrailRenderer DashTrail;
    [SerializeField] private float PostEmitTime;

    private bool _isBusy;
    private bool _isDashing;
    private float _dashTime;
    public float DashCooldownRemaining;

    void OnEnable()
    {
        PlayerActionStarted.Subscribe(() => _isBusy = true, this);
        PlayerActionFinished.Subscribe(() => _isBusy = false, this);
    }

    private void OnDisable()
    {
        PlayerActionStarted.Unsubscribe(this);
        PlayerActionFinished.Unsubscribe(this);
    }

    void FixedUpdate()
    {
        if (_isDashing)
        {
            _dashTime -= Time.deltaTime;
            if (_dashTime <= 0)
            {
                _isDashing = false;
                StartCoroutine(TurnOffEmissionsAfterDelay());
                PlayerActionFinished.Publish();
                DashingFinished.Publish();
            }
        }
        DashCooldownRemaining = Mathf.Max(0, DashCooldownRemaining - Time.deltaTime);
        if (DashCooldownRemaining <= 0 && Input.GetButtonDown("Dash") && !_isBusy
            && new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")).normalized != Vector2.zero)
        {
            _isDashing = true;
            _dashTime = DashLength;
            DashCooldownRemaining = DashCooldown;
            DashTrail.emitting = true;
            PlayerActionStarted.Publish();
            DashingStarted.Publish();
        }
    }

    private IEnumerator TurnOffEmissionsAfterDelay()
    {
        yield return new WaitForSeconds(PostEmitTime);
        DashTrail.emitting = false;
    }
}
