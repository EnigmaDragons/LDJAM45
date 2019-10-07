using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 MoveBy;
    [SerializeField] private float Speed = 1.0f;

    private Vector3 StartPosition;
    private Rigidbody rb;
    private float startTime;
    private float Length;    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartPosition = transform.position;
        MoveBy = transform.position + MoveBy;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Mathf.PingPong(Time.time * Speed, 1);
        // transform.position = Vector3.Lerp(StartPosition, EndPosition, time);
        rb.MovePosition(Vector3.Lerp(StartPosition, MoveBy, time));
    }
}
