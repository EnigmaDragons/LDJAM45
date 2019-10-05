using UnityEngine;

public class AlternateCatController : MonoBehaviour
{
    [SerializeField] private float Speed = 3;
    [SerializeField] private Rigidbody CatBody;
    [SerializeField] private float DashSpeed = 2;
    [SerializeField] private GameEvent OnPlayerDashing;
    [SerializeField] private GameEvent OnPlayerStopDashing;
    [SerializeField] private GameEvent OnSwipingStarted;
    [SerializeField] private GameEvent OnSwipingFinished;

    private bool _isDashing;
    private Vector3 _dashingRotation;
    private bool _isSwiping;

    private void Start()
    {
        OnPlayerDashing.Subscribe(() =>
        {
            _isDashing = true;
            var verticalInput = Input.GetAxis("Vertical");
            var horizontalInput = Input.GetAxis("Horizontal");
            _dashingRotation = Vector3.Normalize(new Vector3(horizontalInput, 0f, verticalInput));
        }, this);
        OnPlayerStopDashing.Subscribe(() => _isDashing = false, this);
        OnSwipingStarted.Subscribe(() => _isSwiping = true, this);
        OnSwipingFinished.Subscribe(() => _isSwiping = false, this);
    }

    private void OnDisable()
    {
        OnPlayerDashing.Unsubscribe(this);
        OnPlayerStopDashing.Unsubscribe(this);
        OnSwipingStarted.Unsubscribe(this);
        OnSwipingFinished.Unsubscribe(this);
    }

    private void FixedUpdate()
    {
        if (_isSwiping)
        {
            CatBody.velocity = Vector3.zero;
        }
        else if (_isDashing)
        {
            transform.forward = _dashingRotation;
            CatBody.velocity = transform.forward * DashSpeed;
        }
        else
        {
            var verticalInput = Input.GetAxis("Vertical");
            var horizontalInput = Input.GetAxis("Horizontal");

            var rotation = Vector3.Normalize(new Vector3(horizontalInput, 0f, verticalInput));
            if (rotation != Vector3.zero)
                transform.forward = rotation;

            if (new Vector2(verticalInput, horizontalInput).normalized == Vector2.zero)
                CatBody.velocity = new Vector3(0, CatBody.velocity.y, 0);
            else
                CatBody.velocity = new Vector3((transform.forward * Speed).x, CatBody.velocity.y, (transform.forward * Speed).z); ;
        }
    }
}
