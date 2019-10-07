using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField, ReadOnly] private bool isOpen = false;
    [SerializeField] private GameEvent openDoorTrigger;
    [SerializeField] private GameEvent closeDoorTrigger;
    [SerializeField] private GameObject openDoor;
    [SerializeField] private GameObject closedDoor;
    [SerializeField] private AudioClip onToggle;
    [SerializeField] private bool startsOpen = false;

    private Camera _gameCamera;
    private bool _isAwake;

    private void Awake()
    {
        _gameCamera = FindObjectOfType<Camera>();
        isOpen = startsOpen;
        UpdateDoorState();
        _isAwake = true;
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
        if (_isAwake && onToggle != null)
            AudioSource.PlayClipAtPoint(onToggle, _gameCamera.transform.position, 0.6f);
    }
}
