using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoombaPathfinding : MonoBehaviour
{
    public Transform[] points;
    
    private NavMeshAgent nav;
    private int destPoint;

    // Start is called before the first frame update
    void Start() {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!nav.pathPending && nav.remainingDistance < 0.5f) {
            GoToNextPoint();
        }            
    }

    void GoToNextPoint() {
        if (points.Length == 0) {
            return;
        }
        nav.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }
}
