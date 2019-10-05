using UnityEngine;

[CreateAssetMenu(fileName = "WeaponName", menuName = "Gun", order = 1)]
class Gun : ScriptableObject
{
    [SerializeField] public float FireInterval = 0.6f;
    [SerializeField] public int NumProjectiles = 1;
    [SerializeField] public GameObject ProjectilePrototype;
    [SerializeField] public float DelayBetweenShots = 0.18f;
}
