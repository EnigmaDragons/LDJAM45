using System;
using UnityEngine;

public class CatPurring : MonoBehaviour
{
    private AudioSource purrAudioSource;

    private void Start()
    {
        purrAudioSource = GetComponent<AudioSource>();
    }

    private void OnMouseEnter()
    {
        if (Math.Abs(Time.timeScale) < 0.01)
        {
            return;
        }

        purrAudioSource.Play();
    }

    private void OnMouseExit()
    {
        if (Math.Abs(Time.timeScale) < 0.01)
        {
            return;
        }

        purrAudioSource.Stop();
    }

    private void Update()
    {
        if (Math.Abs(Time.timeScale) < 0.01 && purrAudioSource.isPlaying)
        {
            purrAudioSource.Stop();
        }
    }
}
