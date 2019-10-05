using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScurry : MonoBehaviour
{
    public float Speed = 5.0f;
    [SerializeField] float WallDistance = 25.0f;

    private float DistanceToWall;
    private GameObject TargetWall;

    // Start is called before the first frame update
    void Start()
    {
        SelectRandomWall();
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetWall == null) {
            Destroy(this.gameObject);
        } else {
            DistanceToWall = Vector3.Distance(transform.position, TargetWall.transform.position);

            if (DistanceToWall > 1) {
                transform.position = Vector3.MoveTowards(transform.position, TargetWall.transform.position, Speed * Time.deltaTime);
                transform.LookAt(TargetWall.transform);
            } else {
                Destroy(this.gameObject);
            }
        }        
    }

    void SelectRandomWall() {
        GameObject[] Walls = GameObject.FindGameObjectsWithTag("Wall");
        try {
            TargetWall = Walls[Random.Range(0, Walls.Length + 1)];
            DistanceToWall = Vector3.Distance(transform.position, TargetWall.transform.position);

            var renderer = TargetWall.transform.GetChild(0).GetComponent<MeshRenderer>();
            if (!renderer.isVisible) {
                Debug.Log("Cannot see TargetWall, reselecting");
                SelectRandomWall();
            }


            if (DistanceToWall < WallDistance) {
                Debug.Log("TargetWall too close");
                SelectRandomWall();
            }
        } catch {
            
        }        
    }
}
