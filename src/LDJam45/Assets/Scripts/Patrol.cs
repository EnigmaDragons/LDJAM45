using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform[] Waypoints;
    private NavMeshAgent Agent;
    private int CurrentWaypoint = 0;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Agent.SetDestination(Waypoints[CurrentWaypoint].position);

        if (Vector3.Distance(transform.position, Waypoints[CurrentWaypoint].position) <= 6.0f)
        {
            CurrentWaypoint++;
            if (CurrentWaypoint == Waypoints.Length)
                CurrentWaypoint = 0;
        }
    }
}
