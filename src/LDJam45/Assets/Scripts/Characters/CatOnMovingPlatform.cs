using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatOnMovingPlatform : MonoBehaviour
{
    private MovingPlatform _platform;
    private GameObject target = null;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {        
    }

    void CheckCollision(Collision collision) {
        if (collision.gameObject.tag == "MovingPlatform" && transform.parent == null) {
            transform.parent = collision.transform;
        } else if (transform.parent != null && collision.gameObject.tag != "MovingPlatform") {
            transform.parent = null;
        }
    }

    private void OnCollisionEnter(Collision collision) {        
        CheckCollision(collision);
    }

    private void OnCollisionStay(Collision collision) {
        CheckCollision(collision);
    }

    private void OnCollisionExit(Collision collision) {
        if (_platform != null) {
            transform.parent = null;
        }        
    }
}
