using UnityEngine;

public class CatPurring : MonoBehaviour
{
    public AudioSource purrAudioSource;

    private void OnMouseEnter()
    {
        purrAudioSource?.Play();
        Debug.Log("Cat purrs.");
    }

    private void OnMouseExit()
    {
        purrAudioSource?.Stop();
        Debug.Log("Cat stops purring.");
    }
}
