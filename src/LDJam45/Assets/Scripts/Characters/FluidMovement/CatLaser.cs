using System;
using UnityEngine;

public class CatLaser : MonoBehaviour
{
    public Action OnFinished;

    [SerializeField] private GunBehaviour Eyes;
    [SerializeField] private float LaserEyesCooldown;
    [SerializeField] private GameEvent OnFired;

    public float LaserEyesCooldownRemaining;

    public void Fire()
    {
        if (LaserEyesCooldownRemaining <= 0)
        {
            LaserEyesCooldownRemaining = LaserEyesCooldown;
            Eyes.Fire();
            OnFired.Publish();
        }
        OnFinished();
    }

    private void Update()
    {
        LaserEyesCooldownRemaining = Mathf.Max(0, LaserEyesCooldownRemaining - Time.deltaTime);
    }
}
