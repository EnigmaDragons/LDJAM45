using UnityEngine;

public class TurnTowardsTarget : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private float RotationSpeed;

    void Update()
    {
        var targetDir = Target.position - transform.position;
        var step = RotationSpeed * Time.deltaTime;
        var newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
