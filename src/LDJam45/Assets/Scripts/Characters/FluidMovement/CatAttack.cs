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
    [SerializeField] private List<Vector3> Forces;
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
        if (_index < Forces.Count)
            _timeRemaing = Timing[_index];
        Weapons.ForEach(x => x.SetActive(true));
        Animator.SetBool("IsAttacking", true);
    }

    public void AttackFinished()
    {
        Weapons.ForEach(x => x.SetActive(false));
        Animator.SetBool("IsAttacking", false);
        OnFinished();
    }

    private int _index = 99;
    private float _timeRemaing;


    private void FixedUpdate()
    {
        if (_index >= Forces.Count)
            return;

        _timeRemaing -= Time.fixedDeltaTime;
        if (_timeRemaing > 0)
            CatBody.AddForce(CatBody.transform.rotation * Forces[_index] * Time.fixedDeltaTime);
        else
        {
            _index++;
            if (_index < Forces.Count)
                _timeRemaing = Timing[_index];
        }
    }
}
