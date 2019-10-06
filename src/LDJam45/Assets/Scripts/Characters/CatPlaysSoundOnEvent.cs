using UnityEngine;

public class CatPlaysSoundOnEvent : MonoBehaviour
{
    [SerializeField] private GameSceneSharedObjects shared;
    [SerializeField] private GameEvent trigger;
    [SerializeField] private AudioClip clip;
    [SerializeField, ReadOnly] private int numTimesTriggered;

    private AudioSource _source;

    private void OnEnable()
    {
        numTimesTriggered = 0;
        _source = shared.catAudioSource;
        trigger.Subscribe(PlaySound, this);
    }

    private void OnDisable()
    {
        trigger.Unsubscribe(this);
    }

    private void PlaySound()
    {
        _source.PlayOneShot(clip, 1f);
        numTimesTriggered++;
    }
}
