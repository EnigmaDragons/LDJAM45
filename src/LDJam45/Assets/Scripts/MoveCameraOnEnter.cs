using UnityEngine;

public class MoveCameraOnEnter : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private GameObject newPosition;
    [SerializeField] private float speed = 90f;

    private bool _isStarted;
    private bool _isFinished;

    private void OnCollisionEnter(Collision collision)
    {
        _isStarted = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        _isStarted = true;
    }

    private void Update()
    {
        if (!_isStarted || _isFinished)
            return;

        if (sceneCamera.transform.position.Equals(newPosition.transform.position))
        {
            _isFinished = true;
            return;
        }

        sceneCamera.transform.position = Vector3.MoveTowards(sceneCamera.transform.position, newPosition.transform.position, speed * Time.deltaTime);
    }
}
