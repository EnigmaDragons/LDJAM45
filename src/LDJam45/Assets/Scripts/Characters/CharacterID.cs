using System;
using UnityEngine;

public class CharacterID : MonoBehaviour
{
    public int ID;

    public void Awake()
    {
        ID = Guid.NewGuid().GetHashCode();
    }
}
