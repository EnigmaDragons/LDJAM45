using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatOnMovingPlatform : MonoBehaviour
{

    Transform _parent;
    Transform _platform;

    private void Start() {
        _parent = this.transform.parent;
        Debug.Log(_parent.ToString());
    }

    private void OnTriggerEnter(Collider other) {
        MovingPlatform isPlatform = other.gameObject.GetComponentInParent<MovingPlatform>();
        if (isPlatform != null) {
            Debug.Log("Setting parent to " + isPlatform.ToString());
            _platform = isPlatform.transform;
            transform.parent = _platform;
        }
    }

    private void OnTriggerExit(Collider other) {
        // MovingPlatform _platform = other.gameObject.GetComponentInParent<MovingPlatform>();
        if (_platform != null) {
            Debug.Log("Unsetting parent transform from " + transform.parent.ToString());
            transform.parent = _parent;
            _platform = null;
        }
    }
}
