using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public bool PlayIronmanMode;
    public bool IsTravelling;
    public int CurrentPlayerHp;
    public int MaxHP;
    public bool DashUnlocked;
    public bool SlashUnlocked;
    public bool RendUnlocked;
    public bool LaserEyesUnlocked;
    public Vector3 LastCheckpoint;
    public Dictionary<int, int> HealthMap;
    public Dictionary<int, bool> IsInvincibleMap;
    public bool DebugMuteMusic;
    public Queue<string> ThoughtsMessageQueue;
    public int CatId;
    public bool IsVictory;
    public bool IsInCutscene;
    public float RunTime;

    public void Win()
    {
        IsVictory = true;
    }

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
        CatId = -1;
        IsVictory = false;
        IsInCutscene = false;
        RunTime = 0;
    }

    public void Gain1PlayerHealth() => HealthMap[CatId] = Math.Min(HealthMap[CatId] + 1, MaxHP);
    public void Lose1PlayerHealth() => HealthMap[CatId] = Math.Min(HealthMap[CatId] - 1, MaxHP - 1);
}
