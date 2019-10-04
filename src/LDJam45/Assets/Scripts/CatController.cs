using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private bool ForceMovement = false;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (ForceMovement) {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed);
        } else {
            Vector3 tempVect = new Vector3(moveHorizontal, 0, moveVertical);
            tempVect = tempVect.normalized * speed * Time.deltaTime;
            rb.MovePosition(transform.position + tempVect); ;
        }               
    }
}
