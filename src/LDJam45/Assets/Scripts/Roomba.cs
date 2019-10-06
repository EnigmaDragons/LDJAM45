using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Roomba : MonoBehaviour
{
    [SerializeField] private GameObject OpenDoor;
    [SerializeField] private GameObject CloseDoor;
    [SerializeField] private GameEvent HealthLostEvent;
    [SerializeField] private float MaxSpeed = 25.0f; 
    [SerializeField] private float SpeedIncrease = 0.5f;
    [SerializeField] private float ChaseDistance = 20.0f;
    [SerializeField] private float ForgetDistance = 40.0f;
    [SerializeField] Transform[] Waypoints;
    [SerializeField] States CurrentState;
    [SerializeField] private bool ShouldChase = true;

    private GameObject player;
    private NavMeshAgent Agent;
    private int CurrentWaypoint = 0;

    private bool Dashed = false;

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
        if (ShouldChase)
        {
            float DistanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            // Debug.Log("Distance to player: " + DistanceFromPlayer.ToString());

            // Chase Player if close
            if (DistanceFromPlayer < ChaseDistance && CurrentState != States.Chasing)
            {
                Debug.Log("The Roomba is Chasing You!");
                CurrentState = States.Chasing;
            }
            else if (DistanceFromPlayer > ForgetDistance && CurrentState != States.Cleaning)
            {
                Debug.Log("The Roomba is cleaning");
                CurrentState = States.Cleaning;
            }

            if (CurrentState == States.Chasing)
            {
                Agent.SetDestination(player.transform.position);
            }
        }

        // Patrol Waypoints
        if (CurrentState == States.Cleaning) {
            Agent.SetDestination(Waypoints[CurrentWaypoint].position);

            // HACK: 5.0f is a magic number based on the roomba mesh radius
            // Debug.Log("Distance to waypoint: " + Vector3.Distance(transform.position, Waypoints[CurrentWaypoint].position));
            if (Vector3.Distance(transform.position, Waypoints[CurrentWaypoint].position) <= 5.0f) {                         
                CurrentWaypoint++; // Set next waypoint

                // Close door
                if (OpenDoor != null && OpenDoor.gameObject.activeSelf) {
                    OpenDoor?.SetActive(false);
                    CloseDoor?.SetActive(true);
                }

                if (CurrentWaypoint == Waypoints.Length) {
                    CurrentWaypoint = 0; // Reset Waypoints
                }
            }
        }

        // Speed increase
        //if (Agent.speed < MaxSpeed) {
        //    Agent.speed += SpeedIncrease * Time.deltaTime;
        //}                       

        if (!Dashed) {
            StartCoroutine(QueueDash());
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            HealthLostEvent.Publish();
        }
    }

    private void OnDestroy() {
        if (CloseDoor == null)
            return;

        CloseDoor.SetActive(false);
        OpenDoor.SetActive(true);
    }

    IEnumerator QueueDash() {
        Dashed = true;
        yield return new WaitForSeconds(10.0f);        
        Agent.velocity = transform.forward * 20.0f;
        Dashed = false;
    }
}
