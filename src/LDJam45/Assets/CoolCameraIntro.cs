using System.Collections.Generic;
using UnityEngine;

public class CoolCameraIntro : MonoBehaviour
{
    [SerializeField] private GameState state;
    [SerializeField] private List<float> durations = new List<float>();
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField, ReadOnly] private bool isFinished;

    [SerializeField, ReadOnly] private Transform _currentStartPoint;
    [SerializeField, ReadOnly] private Transform _nextWaypoint;
    private float _currentDuration = 1f;
    private float _remainingDuration = 1f;
    private int _index = 0;
    private Camera _cam;

    private void OnEnable()
    {
        _cam = FindObjectOfType<Camera>();
        MoveNext();
        _cam.transform.position = _currentStartPoint.position;
        _cam.transform.rotation = _currentStartPoint.rotation;
    }

    private void Start()
    {
        state.IsInCutscene = true;
    }

    void FixedUpdate()
    {
        if (isFinished)
            return;

        _remainingDuration = Mathf.Max(0, _remainingDuration - Time.deltaTime);
        var amount = _remainingDuration / _currentDuration;
        _cam.transform.position = Vector3.Lerp(_nextWaypoint.position, _currentStartPoint.position, amount);
        _cam.transform.rotation = Quaternion.Lerp(_nextWaypoint.rotation, _currentStartPoint.rotation, amount);
        if (_remainingDuration <= 0)
            MoveNext();
    }

    private void MoveNext()
    {
        if (_index >= waypoints.Count - 1)
        {
            isFinished = true;
            state.IsInCutscene = false;
            return;
        }

        _index++;
        _currentStartPoint = waypoints[_index - 1];
        _nextWaypoint = waypoints[_index];
        _currentDuration = durations[_index];
        _remainingDuration = durations[_index];
    }
}
