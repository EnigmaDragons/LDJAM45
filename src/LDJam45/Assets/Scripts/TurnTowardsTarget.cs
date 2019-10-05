using UnityEngine;

public class TurnTowardsTarget : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] public float RotationSpeed;

    void Update()
    {
        if (Target == null)
            return;
        var targetDir = Target.position - transform.position;
        var step = RotationSpeed * Time.deltaTime;
        var newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        var rotation = Quaternion.LookRotation(newDir);
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = rotation;
    }
}
