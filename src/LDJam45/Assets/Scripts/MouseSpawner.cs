using UnityEngine;

public class MouseSpawner : MonoBehaviour
{
    [SerializeField] private GameEvent StartEvent;
    [SerializeField] GameObject MousePrefab;
    [SerializeField] float SpawnTimeMin = 1.0f;
    [SerializeField] private float SpawnTimeMax = 2.0f;
    [SerializeField] private float TotalTime = 60;

    private bool _started;
    private float _timeTilNextSpawn;
    private float _timeRemaining;

    private void Start()
    {
        StartEvent.Subscribe(() =>
        {
            _timeRemaining = TotalTime;
            _started = true;
        }, this);  
    }

    void Update()
    {
        if (!_started)
            return;

        _timeTilNextSpawn -= Time.deltaTime;
        if (_timeTilNextSpawn <= 0)
        {
            Instantiate(MousePrefab, transform.position, Quaternion.identity, transform);
            _timeTilNextSpawn = Random.Range(SpawnTimeMin, SpawnTimeMax);
        }
        _timeRemaining -= Time.deltaTime;
        if (_timeRemaining <= 0)
            _started = false;
    }
}
