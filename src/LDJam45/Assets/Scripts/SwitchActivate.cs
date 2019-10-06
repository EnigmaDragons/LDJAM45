using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivate : MonoBehaviour
{

    [SerializeField] private GameObject InactiveSwitch;
    [SerializeField] private GameObject ActiveSwitch;
    [SerializeField] private GameObject ClosedDoor;
    [SerializeField] private GameObject OpenDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            if (InactiveSwitch.activeSelf && ClosedDoor.activeSelf) {
                InactiveSwitch.SetActive(false);
                ClosedDoor.SetActive(false);

                ActiveSwitch.SetActive(true);
                OpenDoor.SetActive(true);
            }
        }
    }
}
