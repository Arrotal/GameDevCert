using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour
{
    [SerializeField] private Slider _volume, _brightness, _difficulty;
    private readonly float _defaultVolume = 0.5f, _defaultBrightness = 0.5f, _defaultDifficulty = 1;
    void Start()
    {
        _volume.value = PlayerPreferences.GetVolume();
        _brightness.value = PlayerPreferences.GetBrightness();
        _difficulty.value = PlayerPreferences.GetDifficulty();
        musicPlayer  = FindObjectOfType<AudioManager>();
    }

    AudioManager musicPlayer;
    public void SetDefault()
    {
        PlayerPreferences.SetMasterVolume(_defaultVolume);
        _volume.value = _defaultVolume;
        PlayerPreferences.SetMasterBrightness(_defaultBrightness);
        _brightness.value = _defaultBrightness;
        PlayerPreferences.SetMasterDifficulty(_defaultDifficulty);
        _difficulty.value = _defaultDifficulty;
        if (musicPlayer)
        {
            musicPlayer.UpdateVolume();
        }
    }
    public void UpdateVolume()
    {
        PlayerPreferences.SetMasterVolume(_volume.value);
     
    }

    

    public void SaveandExit()
    {
        PlayerPreferences.SetMasterVolume(_volume.value);
        PlayerPreferences.SetMasterBrightness(_brightness.value);
        PlayerPreferences.SetMasterDifficulty(_difficulty.value);
    }

    void Update()
    {
        if (musicPlayer)
        {
            musicPlayer.UpdateVolume();
        }
        else
        {
            Debug.LogWarning("No music found");
        }

    }
}
