using UnityEngine;

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
