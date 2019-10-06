using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAttack : MonoBehaviour
{
    public Action OnFinished;

    [SerializeField] private List<GameObject> Weapons;
    [SerializeField] private Animator Animator;
    [SerializeField] private Rigidbody CatBody;
    [SerializeField] private List<Vector3> Offsets;
    [SerializeField] private List<float> Timing;

    public void Attack(Vector3 direction)
    {
        if (direction.x > 0)
        {
            if (direction.z > 0)
                CatBody.transform.eulerAngles = new Vector3(0, 45, 0);
            else if (direction.z < 0)
                CatBody.transform.eulerAngles = new Vector3(0, 135, 0);
            else
                CatBody.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else if (direction.x < 0)
        {
            if (direction.z > 0)
                CatBody.transform.eulerAngles = new Vector3(0, -45, 0);
            else if (direction.z < 0)
                CatBody.transform.eulerAngles = new Vector3(0, -135, 0);
            else
                CatBody.transform.eulerAngles = new Vector3(0, -90, 0);
        }
        else
        {
            if (direction.z > 0)
                CatBody.transform.eulerAngles = new Vector3(0, 0, 0);
            else if (direction.z < 0)
                CatBody.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        _index = 0;
        _origin = CatBody.transform.position;
        _t = 0;
        if (_index != Offsets.Count)
            _timeToMove = Timing[_index];
        Weapons.ForEach(x => x.SetActive(true));
        Animator.SetBool("IsAttacking", true);
    }

    public void AttackFinished()
    {
        Weapons.ForEach(x => x.SetActive(false));
        Animator.SetBool("IsAttacking", false);
        OnFinished();
    }

    private Vector3 _origin;
    private int _index = 99;
    private float _timeToMove;
    private float _t;


    private void Update()
    {
        if (_index == Offsets.Count)
            return;

        _t += Time.deltaTime / _timeToMove;
        if (_t < 1)
        {
            CatBody.MovePosition(Vector3.Lerp(_origin, _origin + CatBody.transform.rotation * Offsets[_index], _t));
        }
        else
        {
            _index++;
            _origin = CatBody.transform.position;
            _t = 0;
            if (_index < Offsets.Count)
                _timeToMove = Timing[_index];
        }
    }
}
