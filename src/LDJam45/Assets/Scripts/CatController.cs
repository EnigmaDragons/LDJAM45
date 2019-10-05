using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float Speed = 3.0f;
    [SerializeField] private float DashForce = 7.0f;
    [SerializeField] private float JumpForce = 5.0f;
    [SerializeField] private float FallForce = 2.0f;
    [SerializeField] private float Cooldown = 1.0f;

    private bool Jumping = false;
    private bool Dashing = false;
        
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void FixedUpdate() {   
        float MoveVertical = Input.GetAxis("Vertical");
        float MoveHorizontal = Input.GetAxis("Horizontal");
        Vector3 Movement = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;

        // Dashing
        if (!Jumping && !Dashing) {            
            if (Input.GetButton("Dash")) {
                Dashing = true;
                rb.AddForce(Movement * DashForce, ForceMode.Impulse);
                StartCoroutine(CatDash());
            }
        }

        // Jumping
        if (!Jumping && !Dashing) {            
            if (Input.GetButtonDown("Jump")) {
                Jumping = true;
                Movement.y += JumpForce;
                //rb.AddForce(Movement, ForceMode.Impulse);            
                rb.velocity = Vector3.up * JumpForce;
                StartCoroutine(CatJump());                
            }
        }

        // Fall faster during jumps
        if (rb.velocity.y < 0) {
            rb.velocity += Vector3.up * Physics.gravity.y * FallForce * Time.deltaTime;
        }

        // Rotate Cat to moving position
        Vector3 CatRotation = Vector3.Normalize(new Vector3(MoveVertical, 0f, -MoveHorizontal));
        if (CatRotation != Vector3.zero) {
            transform.forward = CatRotation;
        }

        rb.MovePosition(transform.position + Movement * Speed * Time.deltaTime);                 
    }    

    IEnumerator CatJump() {        
        yield return new WaitForSeconds(Cooldown);
        Jumping = false;
    }

    IEnumerator CatDash() {
        yield return new WaitForSeconds(Cooldown);
        Dashing = false;
    }
}
