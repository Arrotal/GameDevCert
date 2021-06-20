using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    AudioSource audioSource;
    
    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();

        audioSource.volume = PlayerPreferences.GetVolume() /10;

    }


}
