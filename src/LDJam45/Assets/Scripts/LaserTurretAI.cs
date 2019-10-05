using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LaserTurretAI : MonoBehaviour
{
    [SerializeField] private WeaponBehaviour LaserGun;
    [SerializeField] private TurnTowardsTarget Turning;

    private LaserAIState _state = LaserAIState.Seeking;
    private float _secondsBeforeFiring = 1;
    private float _secondsBeforeTargetLocked = 3;

    public void Update()
    {
        if (_state == LaserAIState.Seeking)
        {
            _secondsBeforeTargetLocked -= Time.deltaTime;
            if (_secondsBeforeTargetLocked < 0)
            {
                Turning.enabled = false;
                _state = LaserAIState.Firing;
                _secondsBeforeFiring = 1;
            }
        }
        else
        {
            _secondsBeforeFiring -= Time.deltaTime;
            if (_secondsBeforeFiring < 0)
            {
                Turning.enabled = true;
                LaserGun.Fire();
                _state = LaserAIState.Seeking;
                _secondsBeforeTargetLocked = 3;
            }
        }
    }
}

public enum LaserAIState
{
    Seeking,
    Firing
}
