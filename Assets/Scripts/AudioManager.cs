using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private bool _isSoundEffect;
    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();
        if(!_isSoundEffect)
        audioSource.volume = PlayerPreferences.GetVolume();
        if (_isSoundEffect)
        audioSource.volume = PlayerPreferences.GetVolume() /3;
       
    }


    public void UpdateVolume()
    {
        audioSource.volume = PlayerPreferences.GetVolume();
    }
}
