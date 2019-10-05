using UnityEngine;

public class AlternateCatController : MonoBehaviour
{
    [SerializeField] private float Speed = 3;
    [SerializeField] private Rigidbody CatBody;
    [SerializeField] private float DashSpeed = 2;
    [SerializeField] private GameEvent OnPlayerDashing;
    [SerializeField] private GameEvent OnPlayerStopDashing;

    private bool _isDashing;
    private Vector3 _dashingRotation;

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
    }

    private void OnDisable()
    {
        OnPlayerDashing.Unsubscribe(this);
        OnPlayerStopDashing.Unsubscribe(this);
    }

    private void FixedUpdate()
    {
        if (_isDashing)
        {
            transform.forward = _dashingRotation;
            CatBody.velocity = transform.forward * DashSpeed;
            return;
        }

        var verticalInput =  Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");

        var rotation = Vector3.Normalize(new Vector3(horizontalInput, 0f, verticalInput));
        if (rotation != Vector3.zero)
            transform.forward = rotation;

        if (new Vector2(verticalInput, horizontalInput).normalized == Vector2.zero)
            CatBody.velocity = Vector3.zero;
        else
            CatBody.velocity = transform.forward * Speed;
    }
}
