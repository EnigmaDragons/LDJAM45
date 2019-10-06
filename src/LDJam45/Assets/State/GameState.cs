using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public bool IsTravelling;
    public int CurrentHp;
    public int MaxHp = 3;
    public bool DashUnlocked;
    public bool SlashUnlocked;
    public bool RendUnlocked;
    public bool LaserEyesUnlocked;
    public Vector3 LastCheckpoint;

    public void Reset()
    {
        CurrentHp = MaxHp;
        IsTravelling = false;
        DashUnlocked = false;
        SlashUnlocked = false;
        RendUnlocked = false;
        LaserEyesUnlocked = false;
        LastCheckpoint = Vector3.zero;
    }
}
