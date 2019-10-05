using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float Speed = 3.0f;    
    [SerializeField] private float MaxSpeed = 5.0f;

    private float TransitionSpeed;
    private float MovementSpeed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MovementSpeed = Speed;
        TransitionSpeed = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        float MoveVertical = Input.GetAxis("Vertical");
        float MoveHorizontal = Input.GetAxis("Horizontal");        
        Vector3 Movement = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;        

        // Sprinting 
        if (Input.GetKey(KeyCode.LeftShift)) {            
            if (TransitionSpeed < 1) {
                TransitionSpeed += 1.0f * Time.deltaTime;
                MovementSpeed = Mathf.Lerp(Speed, MaxSpeed, TransitionSpeed);                
            }            
        } else {
            if (TransitionSpeed >= 1.0f) {
                TransitionSpeed = 0;
            }
            if (TransitionSpeed < 1 && MovementSpeed > Speed) {               
                TransitionSpeed += 1.0f * Time.deltaTime;
                MovementSpeed = Mathf.Lerp(MovementSpeed, Speed, TransitionSpeed);                
            }
        }

        rb.MovePosition(transform.position + Movement * MovementSpeed * Time.deltaTime);                 
    }
}
