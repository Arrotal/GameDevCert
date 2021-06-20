using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    float backgroundScroller = 0.2f;
    private Material _background;
    Vector2 _offset;
    void Start()
    {
        _background = GetComponent<Renderer>().material;
        _offset = new Vector2( backgroundScroller,0);
    }

    void Update()
    {
        _background.mainTextureOffset += _offset * Time.deltaTime;
    }
}

