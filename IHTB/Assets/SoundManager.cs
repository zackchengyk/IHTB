using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified from https://www.daggerhartlab.com/unity-audio-and-sound-manager-singleton-script/
public class SoundManager : MonoBehaviour
{
	// Audio players components.
	public AudioSource EffectsSource;
	public AudioSource MusicSource;

	// Singleton instance.
	public static SoundManager Audio = null;

	// Initialize the singleton instance.
	private void Awake()
	{
		// If there is not already an instance of SoundManager, set it to this.
		if (Audio == null)
		{
			Audio = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Audio != this)
		{
			Destroy(gameObject);
		}
	}


	// Play a single clip through the sound effects source.
	public void Play(AudioClip clip, float lowPitch = 1, float highPitch = 1)
	{
		float randomPitch = Random.Range(lowPitch, highPitch);

		EffectsSource.pitch = randomPitch;
		EffectsSource.clip = clip;
		EffectsSource.Play();
	}

	// Play a single clip through the music source.
	public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();
	}


	public void RandomSoundEffect(params AudioClip[] clips)
	{
		RandomSoundFromList(clips, 1, 1);
	}

	// Play a random clip from an array, and randomize the pitch slightly.
	public void RandomSoundFromList(AudioClip[] clips, float lowPitch = 1, float highPitch = 1)
	{
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(lowPitch, highPitch);

		EffectsSource.pitch = randomPitch;
		EffectsSource.clip = clips[randomIndex];
		EffectsSource.Play();
	}

}