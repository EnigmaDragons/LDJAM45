using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public bool IsTravelling;

    public void Reset()
    {
        IsTravelling = false;
    }
}
