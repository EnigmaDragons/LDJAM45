using UnityEngine;

public class CatPlaysSoundOnEvent : MonoBehaviour
{
    [SerializeField] private GameSceneSharedObjects shared;
    [SerializeField] private GameEvent trigger;
    [SerializeField] private AudioClip clip;
    [SerializeField, ReadOnly] private int numTimesTriggered;
    
    private void OnEnable()
    {
        numTimesTriggered = 0;
        trigger.Subscribe(PlaySound, this);
    }

    private void OnDisable()
    {
        trigger.Unsubscribe(this);
    }

    private void PlaySound()
    {
        shared.catAudioSource.PlayOneShot(clip, 1f);
        numTimesTriggered++;
    }
}
