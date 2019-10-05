using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roomba : MonoBehaviour
{
    [SerializeField] private float Speed = 20.0f;
    [SerializeField] private float SpeedIncrease = 0.25f;
    [SerializeField] private float DistanceToPlayer = 25.0f;
    [SerializeField] Transform[] Waypoints;
    [SerializeField] States CurrentState;

    private GameObject player;
    private NavMeshAgent Agent;
    private int CurrentWaypoint = 0;

    enum States {
        Cleaning,
        Chasing
    }

    // Start is called before the first frame update
    void Start() {
        CurrentState = States.Cleaning;

        Agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");        
        if (!player) {
            Debug.LogError("Player is not tagged");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Chase Player if close
        if (Vector3.Distance(transform.position, player.transform.position) < DistanceToPlayer) {
            CurrentState = States.Chasing;
        }

        if (CurrentState == States.Chasing) {
            Agent.SetDestination(player.transform.position);
        }

        // Patrol Waypoints
        if (CurrentState == States.Cleaning) {
            Agent.SetDestination(Waypoints[CurrentWaypoint].position);            

            // HACK: 1.6f is a magic number based on the roomba mesh radius
            if (Vector3.Distance(transform.position, Waypoints[CurrentWaypoint].position) <= 1.6f) {                                
                CurrentWaypoint++; // Set next waypoint
                if (CurrentWaypoint == Waypoints.Length) {
                    CurrentWaypoint = 0; // Reset Waypoints
                }
            }
        }                        

        // Speed increase
        if (Agent.speed < Speed) {
            Agent.speed += SpeedIncrease * Time.deltaTime;
        }                       
    }
}
