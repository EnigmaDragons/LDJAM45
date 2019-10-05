using UnityEngine;

public class ClawSwipe : MonoBehaviour
{
    [SerializeField] private GameObject Claw;
    [SerializeField] private Animator Animator;
    [SerializeField] private float SwipeCooldown;
    [SerializeField] private GameEvent PlayerActionStarted;
    [SerializeField] private GameEvent PlayerActionFinished;
    [SerializeField] private GameEvent SwipingStarted;
    [SerializeField] private GameEvent SwipingFinished;

    private bool _isBusy;
    private bool _isSwiping;

    public float SwipeCooldownRemaining;

    public void SwipeFinished()
    {
        _isSwiping = false;
        Claw.SetActive(false);
        Animator.SetBool("IsSwiping", false);
        PlayerActionFinished.Publish();
        SwipingFinished.Publish();
    }

    private void OnEnable()
    {
        PlayerActionStarted.Subscribe(() => _isBusy = true, this);
        PlayerActionFinished.Subscribe(() => _isBusy = false, this);
    }

    private void OnDisable()
    {
        PlayerActionStarted.Unsubscribe(this);
        PlayerActionFinished.Unsubscribe(this);
    }

    private void Update()
    {
        SwipeCooldownRemaining = Mathf.Max(0, SwipeCooldownRemaining - Time.deltaTime);
        if (!_isSwiping && !_isBusy && SwipeCooldownRemaining <= 0 && Input.GetButtonDown("Fire1"))
        {
            SwipeCooldownRemaining = SwipeCooldown;
            _isSwiping = true;
            Claw.SetActive(true);
            Animator.SetBool("IsSwiping", true);
            PlayerActionStarted.Publish();
            SwipingStarted.Publish();
        }
    }
}
