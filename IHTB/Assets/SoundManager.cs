using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Modified from https://www.daggerhartlab.com/unity-audio-and-sound-manager-singleton-script/
public class SoundManager : MonoBehaviour
{
	// Audio players components.
	public AudioSource[] EffectsSource;
	public AudioSource MusicSource;

	[Range(0, 1)]
	public float musicVolume = 0.8f;
	[Range(0, 1)]
	public float effectsVolume = 0.5f; 

	public AudioClip musicStart = null;
	public AudioClip music = null;

	public AudioClip clickSound;


	// Singleton instance.
	public static SoundManager Audio = null;

	// Initialize the singleton instance.
	private void Awake()
	{
		// If there is not already an instance of SoundManager, set it to this.
		if (Audio == null)
		{
			Audio = this;

			if (music != null)
			{
				if (musicStart != null)
				{
					PlayLoopedMusicWithIntro(musicStart, music);
				}
				else
				{
					PlayMusic(music);
				}
			}

			AddButtonClicks();

		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Audio != this)
		{
			Destroy(gameObject);
		}
	}

	public void PlayLoopedMusicWithIntro(AudioClip loopStart, AudioClip toLoop)
	{
		MusicSource.volume = musicVolume;
		MusicSource.PlayOneShot(loopStart);
		MusicSource.PlayScheduled(AudioSettings.dspTime + loopStart.length);
	}

	public AudioSource EmptyEffectsSource()
	{

		for (int i = 0; i < EffectsSource.Length; i++)
		{
			if (!EffectsSource[i].isPlaying)
			{
				return EffectsSource[i];
			}
		}

		return EffectsSource[0];
	}


	public void AddButtonClicks()
	{
		Button[] buttons = Resources.FindObjectsOfTypeAll<Button>();

		foreach (Button button in buttons)
		{
			button.onClick.AddListener(PlayClickSound);
		}
	}

	public void PlayClickSound()
	{
		Play(clickSound, 0.95f, 1.05f);
	}


	// Play a single clip through the sound effects source.
	public void Play(AudioClip clip, float lowPitch = 1, float highPitch = 1)
	{
		float randomPitch = Random.Range(lowPitch, highPitch);

		AudioSource effectSource = EmptyEffectsSource();

		effectSource.volume = effectsVolume;

		effectSource.pitch = randomPitch;
		effectSource.clip = clip;
		effectSource.Play();
	}

	// Play a single clip through the music source.
	public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.volume = musicVolume;
		MusicSource.Play();
	}


	public void PlayRandomSoundEffect(params AudioClip[] clips)
	{
		PlayRandomSoundFromList(clips, 1, 1);
	}

	// Play a random clip from an array, and randomize the pitch slightly.
	public void PlayRandomSoundFromList(AudioClip[] clips, float lowPitch = 1, float highPitch = 1)
	{
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(lowPitch, highPitch);

		AudioSource effectSource = EmptyEffectsSource();

		effectSource.volume = effectsVolume;

		effectSource.pitch = randomPitch;
		effectSource.clip = clips[randomIndex];
		effectSource.Play();
	}

}