using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public bool IsTravelling;
    public int CurrentPlayerHp;
    public bool DashUnlocked;
    public bool SlashUnlocked;
    public bool RendUnlocked;
    public bool LaserEyesUnlocked;
    public Vector3 LastCheckpoint;
    public Dictionary<int, int> HealthMap;
    public Dictionary<int, bool> IsInvincibleMap;
    public bool DebugMuteMusic;
    public Queue<string> ThoughtsMessageQueue;

    public void Reset()
    {
        CurrentPlayerHp = 0;
        IsTravelling = false;
        DashUnlocked = false;
        SlashUnlocked = false;
        RendUnlocked = false;
        LaserEyesUnlocked = false;
        LastCheckpoint = Vector3.zero;
        HealthMap = new Dictionary<int, int>();
        IsInvincibleMap = new Dictionary<int, bool>();
        ThoughtsMessageQueue = new Queue<string>();
    }
}
