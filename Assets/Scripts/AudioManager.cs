using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();

        audioSource.volume = PlayerPreferences.GetVolume();

    }


    public void UpdateVolume()
    {
        audioSource.volume = PlayerPreferences.GetVolume();
    }
}
