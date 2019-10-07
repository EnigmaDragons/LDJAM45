using UnityEngine;

[CreateAssetMenu]
public class LaserTurretAttack : ScriptableObject
{
    public LaserTurretAttackState StartingState;
    public LaserTurretAttackType Type;
    public float Spread;
    public float TimeBetweenShots;
    public float AttackDuration;
    public float RotationSpeed;
    public float WindUpTime;
    public float SeekingTime;
    public float WindDownTime;
}
