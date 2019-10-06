using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpawner : MonoBehaviour
{

    [SerializeField] GameObject MousePrefab;
    [SerializeField] float SpeedIncrease = 0.25f;
    [SerializeField] float SpawnTimer = 1.0f;
    [SerializeField] float SpawnDuration = 60.0f;

    private int MiceSpawned = 0;
    private bool StopSpawning = false;
    private bool HasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (!StopSpawning && !HasSpawned) {
            StartCoroutine(SpawnMouse());
        }
    }

    IEnumerator SpawnMouse() {
        HasSpawned = true;
        GameObject Mouse = Instantiate(MousePrefab, this.transform);
        //MouseScurry Scurry = Mouse.GetComponent<MouseScurry>();
        //Scurry.Speed = MiceSpawned * SpeedIncrease;
        //Debug.Log(Scurry.Speed.ToString());
        MiceSpawned++;
        yield return new WaitForSeconds(SpawnTimer);
        HasSpawned = false;
    }

    IEnumerator SpawnCountdown() {
        yield return new WaitForSeconds(SpawnDuration);
        StopSpawning = true;
    }
}
