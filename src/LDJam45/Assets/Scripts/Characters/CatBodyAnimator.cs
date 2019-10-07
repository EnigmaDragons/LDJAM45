using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class CatBodyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator Animator;

        public void Walk()
        {
            Animator.SetBool("IsWalking", true);
        }

        public void StopWalk()
        {
            Animator.SetBool("IsWalking", false);
        }

        public void AttackRight()
        {
            Animator.SetBool("AttackRight", true);
            StartCoroutine(AttackStarted());
        }

        public void AttackLeft()
        {
            Animator.SetBool("AttackLeft", true);
            StartCoroutine(AttackStarted());
        }

        public void AttackBoth()
        {
            Animator.SetBool("AttackBoth", true);
            StartCoroutine(AttackStarted());
        }

        public IEnumerator AttackStarted()
        {
            yield return new WaitForSeconds(0.05f);
            Animator.SetBool("AttackRight", false);
            Animator.SetBool("AttackLeft", false);
            Animator.SetBool("AttackBoth", false);
        }
    }
}
