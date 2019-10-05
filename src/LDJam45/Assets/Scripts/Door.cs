using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField, ReadOnly] private bool isOpen = false;
    [SerializeField] private GameEvent openDoorTrigger;
    [SerializeField] private GameEvent closeDoorTrigger;
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
        closeDoorTrigger?.Subscribe(CloseDoor, this);
    }

    private void OnDisable()
    {
        openDoorTrigger?.Unsubscribe(this);
        closeDoorTrigger?.Unsubscribe(this);
    }

    public void OpenDoor()
    {
        isOpen = true;
        UpdateDoorState();
    }

    public void CloseDoor()
    {
        isOpen = false;
        UpdateDoorState();
    }

    void UpdateDoorState()
    {
        openDoor.SetActive(isOpen);
        closedDoor.SetActive(!isOpen);
    }
}
