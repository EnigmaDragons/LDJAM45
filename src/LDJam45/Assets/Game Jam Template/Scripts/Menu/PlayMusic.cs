using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayMusic : MonoBehaviour {

    public GameMusicPlayer musicPlayer;
    public MenuSettings menuSettings;
	public AudioMixerSnapshot volumeDown;			//Reference to Audio mixer snapshot in which the master volume of main mixer is turned down
	public AudioMixerSnapshot volumeUp;				//Reference to Audio mixer snapshot in which the master volume of main mixer is turned up

    public AudioSource musicSource => musicPlayer.MusicSource;
	private float resetTime = .01f;					//Very short time used to fade in near instantly without a click


	void Awake () 
	{
        musicPlayer.MusicSource = GetComponent<AudioSource>();
        PlayLevelMusic();
	}

	public void PlayLevelMusic()
	{
		//This switch looks at the last loadedLevel number using the scene index in build settings to decide which music clip to play.
		switch (SceneManager.GetActiveScene().buildIndex)
		{
			//If scene index is 0 (usually title scene) assign the clip titleMusic to musicSource
			case 1:
				musicPlayer.PlaySelectedMusic(menuSettings.mainMenuMusicLoop);
				break;
			//If scene index is 1 (usually main scene) assign the clip mainMusic to musicSource
			case 2:
                musicPlayer.PlaySelectedMusic(menuSettings.musicLoopToChangeTo);
				break;
		}

		//Fade up the volume very quickly, over resetTime seconds (.01 by default)
		FadeUp(resetTime);
		//Play the assigned music clip in musicSource
		
	}
	
	//Used if running the game in a single scene, takes an integer music source allowing you to choose a clip by number and play.
	public void PlaySelectedMusic(AudioClip clipToPlay)
	{
        musicSource.clip = clipToPlay;

		//Play the selected clip
		musicSource.Play ();
	}

	//Call this function to very quickly fade up the volume of master mixer
	public void FadeUp(float fadeTime)
	{
		//call the TransitionTo function of the audioMixerSnapshot volumeUp;
		volumeUp.TransitionTo (fadeTime);
	}

	//Call this function to fade the volume to silence over the length of fadeTime
	public void FadeDown(float fadeTime)
	{
		//call the TransitionTo function of the audioMixerSnapshot volumeDown;
		volumeDown.TransitionTo (fadeTime);
	}
}
