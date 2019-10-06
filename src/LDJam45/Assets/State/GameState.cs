using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public bool IsTravelling;
    public int CurrentHp;
    public int MaxHp = 3;

    public void Reset()
    {
        CurrentHp = MaxHp;
        IsTravelling = false;
    }
}
