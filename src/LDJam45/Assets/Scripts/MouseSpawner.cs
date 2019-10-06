using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpawner : MonoBehaviour
{
    [SerializeField] private GameEvent MouseRoomStarted;
    [SerializeField] GameObject MousePrefab;
    [SerializeField] float SpawnTimer = 1.0f;
    [SerializeField] float SpawnDuration = 15.0f;

    private bool StartSpawing = false;
    private bool StopSpawning = false;
    private bool HasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        MouseRoomStarted.Subscribe(() => StartCoroutine(SpawnCountdown()), this);
    }

    void OnDisable() => MouseRoomStarted.Unsubscribe(this);

    // Update is called once per frame
    void Update()
    {
        if (StartSpawing && !StopSpawning && !HasSpawned)
        {
            StartCoroutine(SpawnMouse());
        }
    }

    IEnumerator SpawnMouse()
    {
        HasSpawned = true;
        Instantiate(MousePrefab, this.transform);
        yield return new WaitForSeconds(SpawnTimer);
        HasSpawned = false;
    }

    IEnumerator SpawnCountdown()
    {
        StartSpawing = true;
        yield return new WaitForSeconds(SpawnDuration);
        StopSpawning = true;
    }
}
