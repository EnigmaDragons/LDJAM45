using UnityEngine;

public class LaserEyes : MonoBehaviour
{
    [SerializeField] private GunBehaviour Eyes;
    [SerializeField] private GameEvent PlayerActionStarted;
    [SerializeField] private GameEvent PlayerActionFinished;
    [SerializeField] private float LaserEyesCooldown;

    private bool _isBusy;

    public float LaserEyesCooldownRemaining;

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
        LaserEyesCooldownRemaining = Mathf.Max(0, LaserEyesCooldownRemaining - Time.deltaTime);
        if (!_isBusy && LaserEyesCooldownRemaining <= 0 && Input.GetButtonDown("Fire2"))
        {
            LaserEyesCooldownRemaining = LaserEyesCooldown;
            Eyes.Fire();
        }
    }
}
