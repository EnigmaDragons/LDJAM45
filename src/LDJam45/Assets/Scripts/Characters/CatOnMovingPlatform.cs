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

    // Update is called once per frame
    void LateUpdate() {        
    }

    private void OnCollisionEnter(Collision collision) {
        _platform = collision.gameObject.GetComponentInParent<MovingPlatform>();
    }

    private void OnCollisionStay(Collision collision) {
        if (_platform != null) {
            transform.parent = collision.transform.parent.transform;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (_platform != null) {
            Debug.Log("Leaving Collosion");
            transform.parent = null;
        }        
    }
}
