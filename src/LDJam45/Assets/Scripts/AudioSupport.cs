using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioSupport : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static AudioMixer audioMixerStatic;

    private void Awake()
    {
        audioMixerStatic = audioMixer;
    }


    public static AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos;                 // set its position

        AudioSource audioSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        audioSource.clip = clip;                                      // define the clip
        audioSource.outputAudioMixerGroup = audioMixerStatic.FindMatchingGroups("SoundFx")[0];

                                                                // set other aSource properties here, if desired
        audioSource.Play();                                     // start the sound
        Destroy(tempGO, clip.length);                           // destroy object after clip duration
        return audioSource;                                     // return the AudioSource reference
    }
}
