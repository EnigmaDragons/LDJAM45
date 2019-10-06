using UnityEngine;

public class AudioDebug : MonoBehaviour
{
    AudioSource[] sources;

    void Start()
    {
        sources = Object.FindObjectsOfType<AudioSource>();
    }

    void Update()
    {
        // When a key is pressed list all the gameobjects that are playing an audio
        if (Input.GetKeyUp(KeyCode.L))
        {

            foreach (AudioSource audioSource in sources)
            {
                if (audioSource.isPlaying) Debug.Log(audioSource.name + " is playing " + audioSource.clip.name);
            }
            Debug.Log("---------------------------"); //to avoid confusion next time
            Debug.Break(); //pause the editor
        }
    }
}
