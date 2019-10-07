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
        if (gameState.PlayIronmanMode)
            CloseDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(HealSleep());
    }

    private void CloseDoor()
    {
        closedDoor.SetActive(true);
        openDoor.SetActive(false);
    }

    private IEnumerator HealSleep()
    {
        if (isFinished)
            yield break;

        CloseDoor();

        AudioSupport.PlayClipAt(sleepSound, gameCamera.transform.position);
        AudioSupport.PlayClipAt(lullaby, gameCamera.transform.position);
        yield return new WaitForSeconds(sleepDuration / 2);
        gameState.HealthMap[gameState.CatId] = gameState.MaxHP;
        yield return new WaitForSeconds(sleepDuration / 2);

        // TODO: Close Door afterwards

        openDoor.SetActive(true);
        closedDoor.SetActive(false);
        isFinished = true;
    }
}
