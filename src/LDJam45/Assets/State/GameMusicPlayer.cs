using UnityEngine;

[CreateAssetMenu]
public class GameMusicPlayer : ScriptableObject
{
    public AudioSource MusicSource;

    public void PlaySelectedMusic(AudioClip clipToPlay)
    {
        MusicSource.clip = clipToPlay;
        MusicSource.Play();
    }
}
