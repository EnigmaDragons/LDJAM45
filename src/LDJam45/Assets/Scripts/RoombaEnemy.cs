using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoombaEnemy : MonoBehaviour
{
    [SerializeField] private GameEvent StartFight;
    [SerializeField] private Patrol Patrol;
    [SerializeField] private Melee Melee;
    [SerializeField] private CharacterID ID;
    [SerializeField] private GameState GameState;
    [SerializeField] private List<RoombaAttack> Stage1Attacks;
    [SerializeField] private List<RoombaAttack> Stage2Attacks;
    [SerializeField] private List<RoombaAttack> Stage3Attacks;
    [SerializeField] private int Stage2Threshhold;
    [SerializeField] private int Stage3Threshhold;

    private Rigidbody _body;
    private NavMeshAgent _agent;
    private Transform _target;

    private bool _fightStarted = false;
    private int _stage = 1;
    private List<RoombaAttack> _currentAttackPattern;
    private int _attackIndex;
    private RoombaAttack _currentAttack;
    private RoombaAttackState _attackState;
    private float _timeTilNextState;

    public void Start()
    {
        StartFight.Subscribe(() =>
        {
            _fightStarted = true;
            Patrol.enabled = false;
        }, this);
        _body = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        Melee.IsDeadly = false;
        _currentAttackPattern = Stage1Attacks;
        _attackIndex = 0;
        UpdateCurrentAttack();
    }

    private void OnDisable() => StartFight.Unsubscribe(this);

    public void Update()
    {
        if (!_fightStarted)
            return;

        if (_stage < 3 && GameState.HealthMap[ID.ID] <= Stage3Threshhold)
        {
            _stage = 3;
            _currentAttackPattern = Stage3Attacks;
            _attackIndex = 0;
            UpdateCurrentAttack();
        }
        else if (_stage < 2 && GameState.HealthMap[ID.ID] <= Stage2Threshhold)
        {
            _stage = 2;
            _currentAttackPattern = Stage2Attacks;
            _attackIndex = 0;
            UpdateCurrentAttack();
        }

        if (_currentAttack.Type == RoombaAttackType.Chase)
            UpdateChase();
        if (_currentAttack.Type == RoombaAttackType.Charge)
            UpdateCharge();
        if (_currentAttack.Type == RoombaAttackType.Spin)
            UpdateSpin();
    }

    private void UpdateChase()
    {
        if (_attackState == RoombaAttackState.Chasing)
            Chase();
        else
        {
            _attackIndex++;
            if (_currentAttackPattern.Count == _attackIndex)
                _attackIndex = 0;
            UpdateCurrentAttack();
        }
    }

    private void UpdateCharge()
    {
        if (_attackState == RoombaAttackState.Chasing)
            Chase();
        else if (_attackState == RoombaAttackState.WindingUp)
            WindUp();
        else if (_attackState == RoombaAttackState.Attacking)
            Charge();
        else if (_attackState == RoombaAttackState.WindingDown)
            WindDown();
    }

    private void Charge()
    {
        Melee.IsDeadly = true;
        _body.AddForce(_body.transform.forward * _currentAttack.Speed * Time.fixedDeltaTime, ForceMode.Force);
        Attack();
    }

    private void UpdateSpin()
    {
        if (_attackState == RoombaAttackState.Chasing)
            Chase();
        else if (_attackState == RoombaAttackState.WindingUp)
            SpinWindUp();
        else if (_attackState == RoombaAttackState.Attacking)
            SpinChase();
        else if (_attackState == RoombaAttackState.WindingDown)
            WindDown();
    }

    private void SpinWindUp()
    {
        _body.AddTorque(new Vector3(0, Time.deltaTime * _currentAttack.RotationSpeed, 0));
        WindUp();
    }

    private void SpinChase()
    {
        Melee.IsDeadly = true;
        _body.AddTorque(new Vector3(0, Time.deltaTime * _currentAttack.RotationSpeed, 0));
        if (_target != null)
            _body.AddForce(Vector3.ClampMagnitude(_target.position - _body.transform.position, 0.1f) * _currentAttack.Speed * Time.deltaTime, ForceMode.Acceleration);
        Attack();
    }

    private void SetToChase()
    {
        Melee.IsDeadly = false;
        _body.isKinematic = true;
        _agent.enabled = true;
        _body.isKinematic = true;
        _attackState = RoombaAttackState.Chasing;
        _timeTilNextState = _currentAttack.ChasingTime;
    }

    private void Chase()
    {
        if (_target != null)
            _agent.SetDestination(_target.position);
        _timeTilNextState -= Time.deltaTime;
        if (_timeTilNextState <= 0)
            SetToWindingUp();
    }

    private void SetToWindingUp()
    {
        _agent.enabled = false;
        _body.isKinematic = false;
        _attackState = RoombaAttackState.WindingUp;
        _timeTilNextState = _currentAttack.WindUpTime;
    }

    private void WindUp()
    {
        Melee.IsDeadly = false;
        _timeTilNextState -= Time.deltaTime;
        if (_timeTilNextState <= 0)
            SetToAttack();
    }

    private void SetToAttack()
    {
        _attackState = RoombaAttackState.Attacking;
        _timeTilNextState = _currentAttack.AttackDuration;
    }

    private void Attack()
    {
        _timeTilNextState -= Time.deltaTime;
        if (_timeTilNextState <= 0)
            SetToWindDown();
    }

    private void SetToWindDown()
    {
        _attackState = RoombaAttackState.WindingDown;
        _timeTilNextState = _currentAttack.WindDownTime;
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
        _agent.speed = _currentAttack.ChaseSpeed;
        _agent.angularSpeed = _currentAttack.ChaseAngularSpeed;
        _agent.acceleration = _currentAttack.ChaseAcceleration;
        if (_attackState == RoombaAttackState.Chasing)
            SetToChase();
        else if (_attackState == RoombaAttackState.WindingUp)
            SetToWindingUp();
        else if (_attackState == RoombaAttackState.Attacking)
            SetToAttack();
        else if (_attackState == RoombaAttackState.WindingDown)
            SetToWindDown();
    }
}

public enum RoombaAttackState
{
    Chasing,
    WindingUp,
    Attacking,
    WindingDown
}

public enum RoombaAttackType
{
    Chase,
    Charge,
    Spin
}

[CreateAssetMenu]
public class RoombaAttack : ScriptableObject
{
    public RoombaAttackState StartingState;
    public RoombaAttackType Type;
    public float AttackDuration;
    public float WindUpTime;
    public float ChasingTime;
    public float WindDownTime;

    public float ChaseSpeed;
    public float ChaseAngularSpeed;
    public float ChaseAcceleration;

    public float Speed;
    public float RotationSpeed;
}
