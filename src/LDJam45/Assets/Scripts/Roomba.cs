using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roomba : MonoBehaviour
{
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
        if (Vector3.Distance(transform.position, player.transform.position) < 25f) {
            CurrentState = States.Chasing;
        }

        if (CurrentState == States.Chasing) {
            Agent.SetDestination(player.transform.position);
        }

        // Patrol Waypoints
        if (CurrentState == States.Cleaning) {
            Agent.SetDestination(Waypoints[CurrentWaypoint].position);            

            if (Vector3.Distance(transform.position, Waypoints[CurrentWaypoint].position) <= 1.6f) {                                
                CurrentWaypoint++;
                if (CurrentWaypoint == Waypoints.Length) {
                    CurrentWaypoint = 0;
                }
            }
        }                        

        if (Agent.speed < 15) {
            Agent.speed += 0.25f * Time.deltaTime;
        }                       
    }
}
