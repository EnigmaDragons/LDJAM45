using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeLevelStore : MonoBehaviour
{
    public AudioMixer mainMixer;

    public SetAudioLevels optionsMenu;
    public SetAudioLevels pauseMenu;

    void Start()
    {
        mainMixer.SetFloat("musicVol", PlayerPrefs.GetFloat("musicVol"));
        mainMixer.SetFloat("sfxVol", PlayerPrefs.GetFloat("sfxVol"));

        mainMixer.SetFloat("masterVol", 0);

        optionsMenu.UpdateVolumeSliders();
        pauseMenu.UpdateVolumeSliders();

        optionsMenu.volumeLoaded = true;
        pauseMenu.volumeLoaded = true;
    }

    private void OnDestroy()
    {
        mainMixer.GetFloat("musicVol", out float musicValue);
        PlayerPrefs.SetFloat("musicVol", musicValue);

        mainMixer.GetFloat("sfxVol", out float sfxValue);
        PlayerPrefs.SetFloat("sfxVol", sfxValue);

        PlayerPrefs.Save();
    }
}
