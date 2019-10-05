using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;    
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;        

        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);                 
    }
}
