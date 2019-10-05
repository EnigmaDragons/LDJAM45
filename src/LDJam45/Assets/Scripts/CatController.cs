using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float Speed = 3.0f;
    [SerializeField] private float DashForce = 1.5f;
    [SerializeField] private float JumpForce = 5.0f;

    private bool Jumping = false;
    private bool Dashing = false;

    private float TransitionSpeed;
    private float MovementSpeed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        MovementSpeed = Speed;
        TransitionSpeed = 0;        
    }

    // Update is called once per frame
    void FixedUpdate() {   
        float MoveVertical = Input.GetAxis("Vertical");
        float MoveHorizontal = Input.GetAxis("Horizontal");

        //Debug.Log("Vertical" + MoveVertical);
        //Debug.Log("Horizontal" + MoveHorizontal);

        Vector3 Movement = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;

        // Dashing
        if (!Jumping && !Dashing) {            
            if (Input.GetButton("Dash")) {
                Dashing = true;
                rb.AddForce(Movement * DashForce, ForceMode.Impulse);
                StartCoroutine(CatDash());
            }
        }

        if (!Jumping && !Dashing) {            
            if (Input.GetButton("Jump")) {
                Jumping = true;
                Movement.y += JumpForce;
                rb.AddForce(Movement, ForceMode.Impulse);                                
                StartCoroutine(CatJump());
            }
        }

        Vector3 CatRotation = Vector3.Normalize(new Vector3(MoveVertical, 0f, -MoveHorizontal));
        if (CatRotation != Vector3.zero) {
            transform.forward = CatRotation;
        }

        rb.MovePosition(transform.position + Movement * MovementSpeed * Time.deltaTime);                 
    }    

    IEnumerator CatJump() {        
        yield return new WaitForSeconds(1.0f);
        Jumping = false;
    }

    IEnumerator CatDash() {
        yield return new WaitForSeconds(1.0f);
        Dashing = false;
    }
}
