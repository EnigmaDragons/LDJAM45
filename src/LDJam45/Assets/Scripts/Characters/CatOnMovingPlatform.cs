using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatOnMovingPlatform : MonoBehaviour
{
    private Rigidbody rb;
    private MovingPlatform _platform;
    private GameObject target = null;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.tag.ToString());
        if (collision.gameObject.tag == "MovingPlatform" && transform.parent == null) {
            transform.parent = collision.transform;
        } else if (transform.parent != null && collision.gameObject.tag != "MovingPlatform") {
            transform.parent = null;
        }
    }

    private void OnCollisionStay(Collision collision) {
        
    }

    private void OnCollisionExit(Collision collision) {    
        transform.parent = null;
    }
}
