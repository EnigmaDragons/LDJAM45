using System.Collections;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] private Gun Gun;
    [SerializeField] private Vector3 ProjectileOffset;
    [SerializeField] private Role Role;

    private float _msBeforeFire = 0;

    private void Update()
    {
        if (_msBeforeFire > 0)
            _msBeforeFire -= Time.deltaTime;
    }

    public void FireTowards(Vector3 target)
    {
        var targetDir = target - transform.position;
        var newDir = Vector3.RotateTowards(transform.forward, targetDir, float.MaxValue, 0.0f);
        var targetRotation = Quaternion.LookRotation(newDir);

        Fire(targetRotation);
    }

    public void Fire()
    {
        Fire(transform.rotation);
    }

    private void Fire(Quaternion rotation)
    {
        if (_msBeforeFire > 0)
            return;

        _msBeforeFire = Gun.FireInterval;
        StartCoroutine(LaunchProjectiles(rotation));
    }

    private IEnumerator LaunchProjectiles(Quaternion rotation)
    {
        for (var i = 0; i < Gun.NumProjectiles; i++)
        {
            var spawnPos = transform.position + transform.forward * ProjectileOffset.z + transform.up * ProjectileOffset.y + transform.right * ProjectileOffset.x;
            var p = Instantiate(Gun.ProjectilePrototype, spawnPos, rotation);
            var projectile = p.GetComponent<ParticleCollisionInstance>();
            projectile.OwnedBy = Role;
            yield return new WaitForSeconds(Gun.DelayBetweenShots);
        }
    }
}
