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
        StartCoroutine(AnimateMovement());
        Weapons.ForEach(x => x.SetActive(true));
        Animator.SetBool("IsAttacking", true);
    }

    public void AttackFinished()
    {
        Weapons.ForEach(x => x.SetActive(false));
        Animator.SetBool("IsAttacking", false);
        OnFinished();
    }

    private IEnumerator AnimateMovement()
    {
        for (var i = 0; i < Offsets.Count; i++)
            yield return MoveByOffset(Offsets[i], Timing[i]);
    }

    private IEnumerator MoveByOffset(Vector3 offset, float timeToMove)
    {
        var start = CatBody.transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            CatBody.transform.position = Vector3.Lerp(start, start + CatBody.transform.rotation * offset, t);
            yield return null;
        }
    }
}
