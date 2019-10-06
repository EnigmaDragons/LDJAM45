
using UnityEngine;

public class PlayCatWalkingSounds : MonoBehaviour
{
    [SerializeField] private GameSceneSharedObjects shared;
    [SerializeField] private GameEvent onStartedMoving;
    [SerializeField] private GameEvent onStoppedMoving;
    [SerializeField] private AudioClip[] steps;
    [SerializeField] private float[] volumes;
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

    private void FixedUpdate()
    {
        _cooldownRemaining = Mathf.Max(0, _cooldownRemaining - Time.deltaTime);
        if (!_isWalking || _cooldownRemaining > 0)
            return;
        
        _cooldownRemaining = timeBetweenSteps;
        index = (index + 1) % steps.Length;
        var stepSound = steps[index];
        var volume = volumes.Length < index ? volumes[index] : 0.5f;
        shared.catAudioSource.PlayOneShot(stepSound, volume);       
    }

}
