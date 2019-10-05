using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField, ReadOnly] private bool isOpen = false;
    [SerializeField] private GameEvent openDoorTrigger;
    [SerializeField] private GameObject openDoor;
    [SerializeField] private GameObject closedDoor;
    [SerializeField] private bool startsOpen = false;

    private void Awake()
    {
        isOpen = startsOpen;
        UpdateDoorState();
    }

    private void OnEnable()
    {
        openDoorTrigger?.Subscribe(OpenDoor, this);
    }

    private void OnDisable()
    {
        openDoorTrigger?.Unsubscribe(this);
    }

    public void OpenDoor()
    {
        isOpen = true;
        UpdateDoorState();
    }

    void UpdateDoorState()
    {
        openDoor.SetActive(isOpen);
        closedDoor.SetActive(!isOpen);
    }
}
