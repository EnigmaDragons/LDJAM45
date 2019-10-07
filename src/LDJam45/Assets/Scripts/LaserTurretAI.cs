using System.Collections.Generic;
using UnityEngine;

public class LaserTurretAI : MonoBehaviour
{
    [SerializeField] private GameEvent FightStarted;
    [SerializeField] private GunBehaviour LaserGun;
    [SerializeField] private TurnTowardsTarget Turning;
    [SerializeField] private Rigidbody LaserTurretBody;
    [SerializeField] private Health Health;
    [SerializeField] private CharacterID ID;
    [SerializeField] private GameState GameState;
    [SerializeField] private List<LaserTurretAttack> Stage1Attacks;
    [SerializeField] private List<LaserTurretAttack> Stage2Attacks;
    [SerializeField] private List<LaserTurretAttack> Stage3Attacks;
    [SerializeField] private int Stage2Threshhold;
    [SerializeField] private int Stage3Threshhold;

    private bool _fightStarted = false;
    private int _stage = 1;
    private List<LaserTurretAttack> _currentAttackPattern;
    private int _attackIndex;
    private LaserTurretAttack _currentAttack;
    private LaserTurretAttackState _attackState;
    private float _timeTilNextState;
    private float _secsTilNextShot;

    private void Start()
    {
        Health.IsInvincible = true;
        FightStarted.Subscribe(() =>
        {
            _fightStarted = true;
            Health.IsInvincible = false;
        }, this);
        _currentAttackPattern = Stage1Attacks;
        _attackIndex = 0;
        _secsTilNextShot = 0;
        UpdateCurrentAttack();
    }

    private void OnDisable() => FightStarted?.Unsubscribe(this);

    public void Update()
    {
        if (!_fightStarted)
            return;

        if (_stage < 3 && GameState.HealthMap[ID.ID] <= Stage3Threshhold)
        {
            _stage = 3;
            _currentAttackPattern = Stage3Attacks;
            _attackIndex = 0;
            _secsTilNextShot = 0;
            UpdateCurrentAttack();
        }
        else if (_stage < 2 && GameState.HealthMap[ID.ID] <= Stage2Threshhold)
        {
            _stage = 2;
            _currentAttackPattern = Stage2Attacks;
            _attackIndex = 0;
            _secsTilNextShot = 0;
            UpdateCurrentAttack();
        }

        if (_currentAttack.Type == LaserTurretAttackType.Volley)
            UpdateVolley();
        else if (_currentAttack.Type == LaserTurretAttackType.Spin)
            UpdateSpin();
    }

    private void UpdateVolley()
    {
        if (_attackState == LaserTurretAttackState.Seeking)
            SeekTarget();
        else if (_attackState == LaserTurretAttackState.WindingUp)
            WindUp();
        else if (_attackState == LaserTurretAttackState.Firing)
            FireVolley();
        else if (_attackState == LaserTurretAttackState.WindingDown)
            WindDown();
    }

    private void FireVolley()
    {
        _secsTilNextShot -= Time.deltaTime;
        if (_secsTilNextShot <= 0)
        {
            var target = transform.position + transform.forward;
            LaserGun.FireTowards(new Vector3(target.x + Random.Range(-_currentAttack.Spread, _currentAttack.Spread),
                target.y, target.z + Random.Range(-_currentAttack.Spread, _currentAttack.Spread)));
            _secsTilNextShot += _currentAttack.TimeBetweenShots;
        }
        _timeTilNextState -= Time.deltaTime;
        if (_timeTilNextState <= 0)
        {
            _secsTilNextShot = 0;
            _attackState = LaserTurretAttackState.WindingDown;
            _timeTilNextState = _currentAttack.WindDownTime;
        }
    }

    private void UpdateSpin()
    {
        if (_attackState == LaserTurretAttackState.Seeking)
            SeekTarget();
        else if (_attackState == LaserTurretAttackState.WindingUp)
            SpinWindUp();
        else if (_attackState == LaserTurretAttackState.Firing)
            SpinFire();
        else if (_attackState == LaserTurretAttackState.WindingDown)
            SpinWindDown();
    }

    private void SpinWindUp()
    {
        LaserTurretBody.AddTorque(new Vector3(0, Time.deltaTime * _currentAttack.RotationSpeed, 0));
        WindUp();
    }

    private void SpinFire()
    {
        LaserTurretBody.AddTorque(new Vector3(0, Time.deltaTime * _currentAttack.RotationSpeed, 0));
        Fire();
    }

    private void SpinWindDown()
    {
        WindDown();
        if (_attackState != LaserTurretAttackState.WindingDown)
            LaserTurretBody.angularVelocity = Vector3.zero;
    }

    private void SeekTarget()
    {
        Turning.enabled = true;
        Turning.RotationSpeed = _currentAttack.RotationSpeed;
        _timeTilNextState -= Time.deltaTime;
        if (_timeTilNextState <= 0)
        {
            _attackState = LaserTurretAttackState.WindingUp;
            _timeTilNextState = _currentAttack.WindUpTime;
            Turning.enabled = false;
        }
    }

    private void WindUp()
    {
        _timeTilNextState -= Time.deltaTime;
        if (_timeTilNextState <= 0)
        {
            _attackState = LaserTurretAttackState.Firing;
            _timeTilNextState = _currentAttack.AttackDuration;
            _secsTilNextShot = 0;
        }
    }

    private void Fire()
    {
        _secsTilNextShot -= Time.deltaTime;
        if (_secsTilNextShot <= 0)
        {
            LaserGun.Fire();
            _secsTilNextShot = _currentAttack.TimeBetweenShots;
        }
        _timeTilNextState -= Time.deltaTime;
        if (_timeTilNextState <= 0)
        {
            _attackState = LaserTurretAttackState.WindingDown;
            _timeTilNextState = _currentAttack.WindDownTime;
        }
    }

    private void WindDown()
    {
        _timeTilNextState -= Time.deltaTime;
        if (_timeTilNextState <= 0)
        {
            _attackIndex++;
            if (_currentAttackPattern.Count == _attackIndex)
                _attackIndex = 0;
            UpdateCurrentAttack();
        }
    }

    private void UpdateCurrentAttack()
    {
        _currentAttack = _currentAttackPattern[_attackIndex];
        _attackState = _currentAttack.StartingState;
        if (_attackState == LaserTurretAttackState.Seeking)
            _timeTilNextState = _currentAttack.SeekingTime;
        else if (_attackState == LaserTurretAttackState.WindingUp)
            _timeTilNextState = _currentAttack.WindUpTime;
        else if (_attackState == LaserTurretAttackState.Firing)
            _timeTilNextState = _currentAttack.AttackDuration;
        else if (_attackState == LaserTurretAttackState.WindingDown)
            _timeTilNextState = _currentAttack.WindDownTime;
    }
}

public enum LaserTurretAttackState
{
    Seeking,
    WindingUp,
    Firing,
    WindingDown
}

public enum LaserTurretAttackType
{
    Volley,
    Spin
}
