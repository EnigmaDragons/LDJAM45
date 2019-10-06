using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioLevels : MonoBehaviour
{
	public AudioMixer mainMixer;					//Used to hold a reference to the AudioMixer mainMixer

    public Slider musicVolSlider;
    public Slider sfxVolSlider;

    private void OnEnable()
    {
        mainMixer.GetFloat("musicVol", out float musicValue);
        musicVolSlider.value = musicValue;

        mainMixer.GetFloat("sfxVol", out float sfxValue);
        sfxVolSlider.value = sfxValue;
    }

    //Call this function and pass in the float parameter musicLvl to set the volume of the AudioMixerGroup Music in mainMixer
    public void SetMusicLevel(float musicLvl)
	{
		mainMixer.SetFloat("musicVol", musicLvl);
	}

	//Call this function and pass in the float parameter sfxLevel to set the volume of the AudioMixerGroup SoundFx in mainMixer
	public void SetSfxLevel(float sfxLevel)
	{
		mainMixer.SetFloat("sfxVol", sfxLevel);
	}
}
