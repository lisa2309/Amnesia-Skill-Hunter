using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource attackSound;
    [SerializeField]
    private AudioSource arrowSound;

    [SerializeField]
    private AudioSource fireSound;
    [SerializeField]
    private Slider slider;

    void Start() {
        attackSound.volume = StateController.currentSoundEffectVolume;
        arrowSound.volume = StateController.currentSoundEffectVolume;
        fireSound.volume = StateController.currentSoundEffectVolume;
        slider.value = StateController.currentSoundEffectVolume;
    }

    public void UpdateVolume(float volume)
	{
		attackSound.volume = volume;
        arrowSound.volume = volume;
        fireSound.volume = volume;
        StateController.currentSoundEffectVolume = volume;
	}
}
