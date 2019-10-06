﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private GameState state;

    private readonly Dictionary<CatAction, DictionaryWithDefault<CatAction, float>> _delays = new Dictionary<CatAction, DictionaryWithDefault<CatAction, float>>
    {
        { CatAction.Moving, new DictionaryWithDefault<CatAction, float>(0) },
        { CatAction.Dash, new DictionaryWithDefault<CatAction, float>(0) },
        { CatAction.StandingSlash, new DictionaryWithDefault<CatAction, float>(0) },
        { CatAction.StandingDoubleSlash, new DictionaryWithDefault<CatAction, float>(0) },
        { CatAction.StandingRend, new DictionaryWithDefault<CatAction, float>(0) },
        { CatAction.MovingSlash, new DictionaryWithDefault<CatAction, float>(0) },
        { CatAction.MovingDoubleSlash, new DictionaryWithDefault<CatAction, float>(0) },
        { CatAction.MovingRend, new DictionaryWithDefault<CatAction, float>(0) },
        { CatAction.DashSlash, new DictionaryWithDefault<CatAction, float>(0) },
        { CatAction.DashRend, new DictionaryWithDefault<CatAction, float>(0) },
        { CatAction.Laser, new DictionaryWithDefault<CatAction, float>(0) },
    };

    private Dictionary<CatAction, Func<Vector3, Action>> _actions;

    [SerializeField] private CatMovement Movement;
    [SerializeField] private CatDash Dash;
    [SerializeField] private CatAttack StandingSlash;
    [SerializeField] private CatAttack StandingDoubleSlash;
    [SerializeField] private CatAttack StandingRend;
    [SerializeField] private CatAttack MovingSlash;
    [SerializeField] private CatAttack MovingDoubleSlash;
    [SerializeField] private CatAttack MovingRend;
    [SerializeField] private CatAttack DashSlash;
    [SerializeField] private CatAttack DashRend;
    [SerializeField] private CatLaser Laser;

    private DirectionalCatAction _currentAction = new DirectionalCatAction(CatAction.Moving, Vector3.zero);
    private DirectionalCatAction _queuedAction = new DirectionalCatAction(CatAction.Moving, Vector3.zero);
    private bool _doingAction = false;

    private void Start()
    {
        _actions = new Dictionary<CatAction, Func<Vector3, Action>>
        {
            { CatAction.Moving, dir => () => Movement.IsActive = true },
            { CatAction.Dash, dir => () => Dash.Dash(dir) },
            { CatAction.StandingSlash, dir => () => StandingSlash.Attack(dir) },
            { CatAction.StandingDoubleSlash, dir => () => StandingDoubleSlash.Attack(dir) },
            { CatAction.StandingRend, dir => () => StandingRend.Attack(dir) },
            { CatAction.MovingSlash, dir => () => MovingSlash.Attack(dir) },
            { CatAction.MovingDoubleSlash, dir => () => MovingDoubleSlash.Attack(dir) },
            { CatAction.MovingRend, dir => () => MovingRend.Attack(dir) },
            { CatAction.DashSlash, dir => () => DashSlash.Attack(dir) },
            { CatAction.DashRend, dir => () => DashRend.Attack(dir) },
            { CatAction.Laser, dir => () => Laser.Fire() },
        };

        Dash.OnFinished = OnFinishedWithCurrentAction;
        StandingSlash.OnFinished = OnFinishedWithCurrentAction;
        StandingDoubleSlash.OnFinished = OnFinishedWithCurrentAction;
        StandingRend.OnFinished = OnFinishedWithCurrentAction;
        MovingSlash.OnFinished = OnFinishedWithCurrentAction;
        MovingDoubleSlash.OnFinished = OnFinishedWithCurrentAction;
        MovingRend.OnFinished = OnFinishedWithCurrentAction;
        DashSlash.OnFinished = OnFinishedWithCurrentAction;
        DashRend.OnFinished = OnFinishedWithCurrentAction;
        Laser.OnFinished = OnFinishedWithCurrentAction;
    }

    private void Update()
    {
        if (Input.GetKey("z") && Input.GetKey("v") && Input.GetKey("x"))
        {
            state.DashUnlocked = true;
            state.SlashUnlocked = true;
            state.RendUnlocked = true;
            state.LaserEyesUnlocked = true;
        }

        var direction = Vector3.ClampMagnitude(Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))), 1f);

        if (state.DashUnlocked && Input.GetButtonDown("Dash"))
            _queuedAction = new DirectionalCatAction(CatAction.Dash, direction);
        else if (state.LaserEyesUnlocked && Input.GetButtonDown("FireLaser"))
            _queuedAction = new DirectionalCatAction(CatAction.Laser, direction);
        else if (state.SlashUnlocked && Input.GetButtonDown("Fire1"))
        {
            if (_currentAction.Action == CatAction.Dash)
                _queuedAction = new DirectionalCatAction(CatAction.DashSlash, _currentAction.Direction);
            else if (_currentAction.Action == CatAction.StandingSlash || _currentAction.Action == CatAction.MovingSlash)
                _queuedAction = new DirectionalCatAction(direction == Vector3.zero ? CatAction.StandingDoubleSlash : CatAction.MovingDoubleSlash, direction);
            else
                _queuedAction = new DirectionalCatAction(direction == Vector3.zero ? CatAction.StandingSlash : CatAction.MovingSlash, direction);
        }
        else if (state.RendUnlocked && Input.GetButtonDown("Fire2"))
        {
            if (_currentAction.Action == CatAction.Dash)
                _queuedAction = new DirectionalCatAction(CatAction.DashRend, _currentAction.Direction);
            else
                _queuedAction = new DirectionalCatAction(direction == Vector3.zero ? CatAction.StandingRend : CatAction.MovingRend, direction);
        }

        if (!_doingAction && _queuedAction.Action != CatAction.Moving)
                OnFinishedWithCurrentAction();
    }

    private void OnFinishedWithCurrentAction()
    {
        StartCoroutine(TransitionToNextAction(_delays[_currentAction.Action][_queuedAction.Action], _actions[_queuedAction.Action](_queuedAction.Direction)));
        _currentAction = _queuedAction;
        _queuedAction = new DirectionalCatAction(CatAction.Moving, Vector3.zero);
        _doingAction = _currentAction.Action != CatAction.Moving;
    }

    private IEnumerator TransitionToNextAction(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        Movement.IsActive = false;
        action();
    }
}

public class DirectionalCatAction
{
    public CatAction Action;
    public Vector3 Direction;

    public DirectionalCatAction(CatAction action, Vector3 direction)
    {
        Action = action;
        Direction = direction;
    }
}

public enum CatAction
{
    Dash,
    StandingSlash,
    StandingDoubleSlash,
    StandingRend,
    MovingSlash,
    MovingDoubleSlash,
    MovingRend,
    DashSlash,
    DashRend,
    Moving,
    Laser
}
