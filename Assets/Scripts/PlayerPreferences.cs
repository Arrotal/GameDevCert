using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreferences : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "Master Volume", BRIGHTNESS_KEY = "Brightness", DIFFICULTY_KEY = "Difficulty";
    const float MIN_VOLUME = 0, MAX_VOLUME = 1, MIN_BRIGHTNESS =-2, MAX_BRIGHTNESS = 2, MIN_DIFFICULTY = 1, MAX_DIFFICULTY = 5;
    public static void SetMasterVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Outside volume constraints");
        }
    }

    public static void SetMasterBrightness(float brightness)
    {
        if (brightness >= MIN_BRIGHTNESS && brightness <= MAX_BRIGHTNESS)
        {

            PlayerPrefs.SetFloat(BRIGHTNESS_KEY, brightness);
        }
        else
        {
            Debug.LogError("Outside Brightness constraints");
        }
    }

    public static void SetMasterDifficulty(float difficulty)
    {
        if (difficulty >= MIN_DIFFICULTY && difficulty <= MAX_DIFFICULTY)
        {

            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("Outside Difficulty constraints");
        }
    }

    public static float GetVolume()
    {

        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static float GetBrightness()
    {

        return PlayerPrefs.GetFloat(BRIGHTNESS_KEY);
    }

    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }
}
