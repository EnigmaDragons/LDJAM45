using UnityEngine;

public class SwitchMusicWhen : MonoBehaviour
{
    [SerializeField] private GameMusicPlayer player;
    [SerializeField] private AudioClip chillMusic;
    [SerializeField] private GameEvent[] shouldChill;
    [SerializeField] private AudioClip fightMusic;
    [SerializeField] private GameEvent[] fightStarted;
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private GameEvent[] bossStarted;

    private Mood _mood = Mood.Chill;

    private void OnEnable()
    {
        shouldChill?.ForEach(e => e.Subscribe(PlayChill, this));
        fightStarted?.ForEach(e => e.Subscribe(PlayFightMusic, this));
        bossStarted?.ForEach(e => e.Subscribe(PlayBossMusic, this));
    }

    private void OnDisable()
    {
        shouldChill?.ForEach(e => e.Unsubscribe(this));
        fightStarted?.ForEach(e => e.Unsubscribe(this));
        bossStarted?.ForEach(e => e.Unsubscribe(this));
    }

    private void PlayChill()
    {
        if (_mood == Mood.Chill)
            return;

        _mood = Mood.Chill;
        player.PlaySelectedMusic(chillMusic);
    }

    private void PlayFightMusic()
    {
        if (_mood == Mood.Action)
            return;

        _mood = Mood.Action;
        player.PlaySelectedMusic(fightMusic);
    }

    private void PlayBossMusic()
    {
        if (_mood == Mood.Boss)
            return;

        _mood = Mood.Boss;
        player.PlaySelectedMusic(bossMusic);
    }

    private enum Mood
    {
        Chill,
        Action,
        Boss
    }
}
