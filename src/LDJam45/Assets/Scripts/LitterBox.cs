using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterBox : MonoBehaviour
{
    [SerializeField] GameObject DirtyLitterBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (!DirtyLitterBox.activeSelf) {
                DirtyLitterBox.SetActive(true);
            }
        }
        
        Debug.Log(other.gameObject.tag.ToString());
    }
}
