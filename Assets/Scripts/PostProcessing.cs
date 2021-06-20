using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
public class PostProcessing : MonoBehaviour
{
    [SerializeField] private PostProcessVolume _volume;
    private AutoExposure _auto;
    [SerializeField] private Slider _brightness; 

    void Awake()
    {
        _volume.GetComponent<PostProcessVolume>();
        _volume.profile.TryGetSettings<AutoExposure>(out _auto);
    }
    private void Update()
    {

        _auto.minLuminance.value = PlayerPreferences.GetBrightness();
    }
    public void UpdateBrightness()
    {

        _volume.profile.TryGetSettings<AutoExposure>(out _auto);
        PlayerPreferences.SetMasterBrightness(_brightness.value);

    
        _auto.minLuminance.value = PlayerPreferences.GetBrightness();   
    }
}
