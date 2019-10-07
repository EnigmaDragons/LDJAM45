using System.Collections;
using UnityEngine;

public class CatHouseHeals : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject openDoor;
    [SerializeField] private GameObject closedDoor;
    [SerializeField] private float sleepDuration = 1.5f;
    [SerializeField] private AudioClip sleepSound;
    [SerializeField] private AudioClip lullaby;

    [SerializeField, ReadOnly] private bool isFinished;

    private Camera gameCamera;

    private void Awake()
    {
        isFinished = false;
        gameCamera = FindObjectOfType<Camera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(HealSleep());
    }

    private IEnumerator HealSleep()
    {
        if (isFinished)
            yield break;

        closedDoor.SetActive(true);
        openDoor.SetActive(false);

        AudioSource.PlayClipAtPoint(sleepSound, gameCamera.transform.position);
        AudioSource.PlayClipAtPoint(lullaby, gameCamera.transform.position);
        yield return new WaitForSeconds(sleepDuration / 2);
        gameState.HealthMap[gameState.CatId]++;
        yield return new WaitForSeconds(sleepDuration / 2);

        // TODO: Close Door afterwards

        openDoor.SetActive(true);
        closedDoor.SetActive(false);
        isFinished = true;
    }
}
