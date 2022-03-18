using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Slider slider;

    private void Start()
    {
        audioSource.volume = StateController.currentMusicVolume;
        slider.value = StateController.currentMusicVolume;
        audioSource.Play();
        
    }

	public void UpdateVolume(float volume)
	{
		audioSource.volume = volume;
        StateController.currentMusicVolume = volume;
	}

}