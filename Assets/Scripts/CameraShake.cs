using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Camera _camera; // set this via inspector
    private float _shake = 1, shakeAmount = 0.1f, _decreaseFactor = 1.0f;

    private static CameraShake _instance;
        public static CameraShake Instance
        {
            get
        {
            if(_instance == null)
        {
            Debug.LogError("CameraShake failed to load");
        }
            return _instance;
    }
}
    public void Shake()
    {
        StartCoroutine(StartShake());
    }
    private Vector3 _shakevector;
    private IEnumerator StartShake()
    {
        _shake = 1;
        while (_shake > 0)
        {

            if (_shake > 0)
            {
                _shakevector = Random.insideUnitSphere * shakeAmount;
                _shakevector.z = -10;
                _camera.transform.localPosition = _shakevector;
                _shake -= Time.deltaTime * _decreaseFactor;

            }
            else
            {
                _shake = 0f;
            }
            yield return null;
        }
        _camera.transform.localPosition = new Vector3(0, 0, -10);
    }
    private void Awake()
    {
        _instance = this;
        _camera = GetComponent<Camera>();
    }
  
}
