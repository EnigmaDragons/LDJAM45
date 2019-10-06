using System;
using UnityEngine;

public class CatPurring : MonoBehaviour
{
    public AudioSource purrAudioSource;

    private void OnMouseEnter()
    {
        if (Math.Abs(Time.timeScale) < 0.01)
        {
            return;
        }

        //purrAudioSource.Play();
        Debug.Log("Cat purrs.");
    }

    private void OnMouseExit()
    {
        if (Math.Abs(Time.timeScale) < 0.01)
        {
            return;
        }

        //purrAudioSource.Stop();
        Debug.Log("Cat stops purring.");
    }
}
