using System;
using UnityEngine;

public class CharacterID : MonoBehaviour
{
    [SerializeField, ReadOnly] public int ID;

    public void Awake()
    {
        ID = Guid.NewGuid().GetHashCode();
    }
}
