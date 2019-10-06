
using UnityEngine;

public class PlayCatWalkingSounds : MonoBehaviour
{
    [SerializeField] private GameSceneSharedObjects shared;
    [SerializeField] private GameEvent onStartedMoving;
    [SerializeField] private GameEvent onStoppedMoving;
    [SerializeField] private AudioClip[] steps;
    [SerializeField] private float timeBetweenSteps = 0.3f;

    private float _cooldownRemaining;
    private bool _isWalking;
    private int index;

    private void OnEnable()
    {
        onStartedMoving.Subscribe(() => _isWalking = true, this);
        onStoppedMoving.Subscribe(() => _isWalking = false, this);
    }

    private void OnDisable()
    {
        onStartedMoving.Unsubscribe(this);
        onStoppedMoving.Unsubscribe(this);
    }

    private void Update()
    {
        _cooldownRemaining = Mathf.Max(0, _cooldownRemaining - Time.deltaTime);
        if (!_isWalking)
            return;

        _cooldownRemaining = timeBetweenSteps;
        index = (index + 1) % steps.Length;
        var stepSound = steps[index];
        shared.catAudioSource.PlayOneShot(stepSound);       
    }

}
