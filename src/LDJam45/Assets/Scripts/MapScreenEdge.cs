using UnityEngine;

public enum MapMoveDirection
{
    Left,
    Right,
    Up,
    Down
}

public class MapScreenEdge : MonoBehaviour
{
    [SerializeField] private GameSceneSharedObjects shared;
    [SerializeField] private GameState state;
    [SerializeField] private MapMoveDirection direction;
    [SerializeField, ReadOnly] private bool isTravelling;
    [SerializeField] private GameObject permablock;

    private const float speed = 150f;

    private Vector3 _positionDelta;
    private Vector3 _targetPosition;
    private bool _isStarted;
    private bool _isFinished;
    private bool _everTriggered;

    private void OnCollisionEnter(Collision collision) => Trigger(collision.gameObject);
    private void OnTriggerEnter(Collider other) => Trigger(other.gameObject);
    private void OnCollisionExit(Collision collision) => FinishedTravel(collision.gameObject);
    private void OnTriggerExit(Collider other) => FinishedTravel(other.gameObject);

    private void Awake()
    {
        float deltaZ = 0f;
        if (direction == MapMoveDirection.Up)
            deltaZ = shared.ScreenHeightUnits;
        if (direction == MapMoveDirection.Down)
            deltaZ = -shared.ScreenHeightUnits;
        var deltaX = 0f;
        if (direction == MapMoveDirection.Right)
            deltaX = shared.ScreenWidthUnits;
        if (direction == MapMoveDirection.Left)
            deltaX = -shared.ScreenWidthUnits;
        _positionDelta = new Vector3(deltaX, 0, deltaZ);
    }

    private void Trigger(GameObject other)
    {
        if (_everTriggered || !other.name.Equals("PlayerCat") || state.IsTravelling)
            return;

        _isStarted = true;
        _everTriggered = true;
        _targetPosition = shared.gameCamera.transform.position + _positionDelta;
        state.IsTravelling = true;
        isTravelling = true;

        permablock.SetActive(true);
    }

    private void FinishedTravel(GameObject other)
    {
        state.IsTravelling = false;
        isTravelling = false;
        permablock.SetActive(true);
    }

    private void Update()
    {
        if (!_isStarted || _isFinished)
            return;

        var c = shared.gameCamera;
        if (c.transform.position.Equals(_targetPosition))
        {
            _isFinished = true;
            return;
        }

        c.transform.position = Vector3.MoveTowards(c.transform.position, _targetPosition, speed * Time.deltaTime);
    }
}
