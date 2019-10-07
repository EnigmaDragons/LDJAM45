using UnityEngine;

public class MouseSpawner : MonoBehaviour
{
    [SerializeField] GameObject MousePrefab;
    [SerializeField] float SpawnTimeMin = 1.0f;
    [SerializeField] private float SpawnTimeMax = 2.0f;

    private float _timeTilNextSpawn;

    void Update()
    {
        _timeTilNextSpawn -= Time.deltaTime;
        if (_timeTilNextSpawn <= 0)
        {
            Instantiate(MousePrefab, transform.position, Quaternion.identity, transform);
            _timeTilNextSpawn = Random.Range(SpawnTimeMin, SpawnTimeMax);
        }
    }
}
