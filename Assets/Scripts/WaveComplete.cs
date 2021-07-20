using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveComplete : MonoBehaviour
{
    [SerializeField] private Canvas _waveCompleteRecapCanvas;
    public void LoadWaveCompleteCanvas()
    {
        _waveCompleteRecapCanvas.enabled = true;
    }
}
