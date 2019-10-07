using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatIsOnGround : MonoBehaviour
{
    private readonly List<Collider> Grounds = new List<Collider>();

    private Collider _catCollider;

    public bool IsOnGround;

    private void Start()
    {
        _catCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
            Grounds.Add(other.collider);
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
            Grounds.Remove(other.collider);
    }

    private void Update()
    {
        if (!Grounds.Any())
            IsOnGround = false;
        else
            IsOnGround = _catCollider.bounds.center.y - _catCollider.bounds.extents.y + 0.03 >=
                         Grounds[0].bounds.center.y + Grounds[0].bounds.extents.y;
    }
} 
