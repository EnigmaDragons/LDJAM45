using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterBox : MonoBehaviour
{
    [SerializeField] GameObject DirtyLitterBox;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (!DirtyLitterBox.activeSelf) {
                DirtyLitterBox.SetActive(true);
            }
        }               
    }
}
