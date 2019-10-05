using System;
using UnityEngine;

public class ClawSwipe : MonoBehaviour
{
    [SerializeField] private GameObject Claw;
    [SerializeField] private Animator Animator;
    [SerializeField] private float SwipeCooldown;
    [SerializeField] private GameEvent OnSwipingStarted;
    [SerializeField] private GameEvent OnSwipingFinished;
    [SerializeField] private GameEvent OnPlayerDashing;
    [SerializeField] private GameEvent OnPlayerStopDashing;

    private bool _isSwiping;
    private bool _isDashing;

    public float SwipeCooldownRemaining;

    public void SwipeFinished()
    {
        _isSwiping = false;
        Claw.SetActive(false);
        Animator.SetBool("IsSwiping", false);
        OnSwipingFinished.Publish();
    }

    private void OnEnable()
    {
        OnPlayerDashing.Subscribe(() => _isDashing = true, this);
        OnPlayerStopDashing.Subscribe(() => _isDashing = false, this);
    }

    private void OnDisable()
    {
        OnPlayerDashing.Unsubscribe(this);
        OnPlayerStopDashing.Unsubscribe(this);
    }

    private void Update()
    {
        SwipeCooldownRemaining = Mathf.Max(0, SwipeCooldownRemaining - Time.deltaTime);
        if (!_isSwiping && !_isDashing && SwipeCooldownRemaining <= 0 && Input.GetButtonDown("Fire1") && Math.Abs(Time.timeScale) > 0.01)
        {
            SwipeCooldownRemaining = SwipeCooldown;
            _isSwiping = true;
            Claw.SetActive(true);
            Animator.SetBool("IsSwiping", true);
            OnSwipingStarted.Publish();
        }
    }
}
