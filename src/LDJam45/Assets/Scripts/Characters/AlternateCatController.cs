using UnityEngine;

public class AlternateCatController : MonoBehaviour
{
    [SerializeField] private float Speed = 5.0f;    
    [SerializeField] private float DashSpeed = 5.0f;

    [SerializeField] private GameEvent OnPlayerDashing;
    [SerializeField] private GameEvent OnPlayerStopDashing;
    [SerializeField] private GameEvent OnSwipingStarted;
    [SerializeField] private GameEvent OnSwipingFinished;

    private bool _isDashing;
    private bool _isSwiping;
    
    private Rigidbody CatBody;
    private Animator Animator;
    private Vector3 rotation;
    private Vector3 _inputs = Vector3.zero;

    private void Start()
    {
        CatBody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();

        OnPlayerDashing.Subscribe(() =>
        {
            _isDashing = true;
            CatBody.useGravity = false;
        }, this);
        OnPlayerStopDashing.Subscribe(() =>
        {
            _isDashing = false;
            CatBody.useGravity = true;
        }, this);

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

    private void Update()
    {
        
        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.z = Input.GetAxis("Vertical");
        rotation = Vector3.Normalize(new Vector3(_inputs.x, 0f, _inputs.z));

        
        if (_isDashing)
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward, DashSpeed * new Vector3(
                (Mathf.Log(1f / (Time.deltaTime * CatBody.drag + 1)) / -Time.deltaTime),
                0,
                (Mathf.Log(1f / (Time.deltaTime * CatBody.drag + 1)) / -Time.deltaTime)));
            CatBody.AddForce(dashVelocity, ForceMode.VelocityChange);
        }        

        if (rotation != Vector3.zero && !_isSwiping && _inputs != Vector3.zero)
            transform.forward = rotation;   
    }

    private void FixedUpdate() {
        if (_isSwiping || _inputs == Vector3.zero) {
            Animator.SetBool("IsWalking", false);
            CatBody.velocity = new Vector3(0, CatBody.velocity.y, 0);
        }
        else {
            Animator.SetBool("IsWalking", true);
            CatBody.MovePosition(CatBody.position + _inputs * Speed * Time.fixedDeltaTime);
        }
    }
}
