using UnityEngine;

[CreateAssetMenu]
public class GameMusicPlayer : ScriptableObject
{
    public AudioSource MusicSource;
    public bool DebugDisableMusic;

    public void PlaySelectedMusic(AudioClip clipToPlay)
    {
        if (DebugDisableMusic)
            return;

        MusicSource.clip = clipToPlay;
        MusicSource.Play();
    }
}
