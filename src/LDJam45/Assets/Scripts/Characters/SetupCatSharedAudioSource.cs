using UnityEngine;

public class SetupCatSharedAudioSource : MonoBehaviour
{
    [SerializeField] private GameSceneSharedObjects shared;

    private void Awake()
    {
        shared.catAudioSource = GetComponent<AudioSource>();
    }
}
