using UnityEngine;

[CreateAssetMenu(fileName = "WeaponName", menuName = "Weapon", order = 1)]
class Weapon : ScriptableObject
{
    [SerializeField] public float FireInterval = 0.6f;
    [SerializeField] public int NumProjectiles = 1;
    [SerializeField] public GameObject ProjectilePrototype;
    [SerializeField] public int Damage;
    [SerializeField] public float DelayBetweenShots = 0.18f;
}
