using System.Collections;
using UnityEngine;

public class CatHouseHeals : MonoBehaviour
{
    [SerializeField] private GameObject openDoor;
    [SerializeField] private GameObject closedDoor;
    [SerializeField] private float sleepDuration = 1.5f;

    [SerializeField, ReadOnly] private bool isFinished;

    private void Awake()
    {
        isFinished = false;
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

        // TODO: Play Sleep Sound
        yield return new WaitForSeconds(sleepDuration / 2);
        // TODO: Heal One Health
        yield return new WaitForSeconds(sleepDuration / 2);

        // TODO: Close Door afterwards

        openDoor.SetActive(true);
        closedDoor.SetActive(false);
        isFinished = true;
    }
}
