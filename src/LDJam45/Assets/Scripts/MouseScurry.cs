using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScurry : MonoBehaviour
{
    public float Speed = 5.0f;
    [SerializeField] float WallDistance = 35.0f;

    private float DirectionToTarget;
    private GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        SelectRandomTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null) {
            Destroy(this.gameObject);
        } else {
            DirectionToTarget = Vector3.Distance(transform.position, _target.transform.position);

            if (DirectionToTarget > 1) {
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Speed * Time.deltaTime);
                transform.LookAt(_target.transform);
            } else {
                Destroy(this.gameObject);
            }
        }        
    }

    void SelectRandomTarget() {
        GameObject[] Targets = GameObject.FindGameObjectsWithTag("MouseSpawner");
        try {
            _target = Targets[Random.Range(0, Targets.Length)];
            DirectionToTarget = Vector3.Distance(transform.position, _target.transform.position);

            var renderer = _target.transform.GetChild(0).GetComponent<MeshRenderer>();
            if (!renderer.isVisible) {
                Debug.Log("Cannot see TargetWall, reselecting");
                SelectRandomTarget();
            }


            if (DirectionToTarget < WallDistance) {
                Debug.Log("TargetWall too close");
                SelectRandomTarget();
            }
        } catch {
            
        }        
    }
}
