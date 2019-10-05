using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roomba : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        if (!player) {
            Debug.LogError("Player is not tagged");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.speed < 15) {
            agent.speed += 0.5f * Time.deltaTime;
        }        
        agent.SetDestination(player.transform.position);
    }
}
