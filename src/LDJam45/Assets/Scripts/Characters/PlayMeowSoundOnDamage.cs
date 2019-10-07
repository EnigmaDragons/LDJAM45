using UnityEngine;

class PlayMeowSoundOnDamage : MonoBehaviour
{
    [SerializeField] private GameSceneSharedObjects shared;
    [SerializeField] private GameEvent onDamaged;
    [SerializeField] private AudioClip[] sounds;

    void OnEnable()
    {
        onDamaged.Subscribe(PlaySound, this);
    }

    private void OnDisable()
    {
        onDamaged.Unsubscribe(this);
    }

    private void PlaySound()
    {
        Debug.Log("Ouch");
        shared.catAudioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
    }
}
