using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource attackSound;
    [SerializeField]
    private AudioSource arrowSound;

    [SerializeField]
    private AudioSource fireSound;

    public void UpdateVolume(float volume)
	{
		attackSound.volume = volume;
        arrowSound.volume = volume;
        fireSound.volume = volume;
	}
}
