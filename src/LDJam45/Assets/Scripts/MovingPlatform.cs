using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 EndPosition;
    [SerializeField] private float Speed = 1.0f;

    private Vector3 StartPosition;
    private float startTime;
    private float Length;    

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = this.transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Mathf.PingPong(Time.time * Speed, 1);
        transform.position = Vector3.Lerp(StartPosition, EndPosition, time);
    }
}
