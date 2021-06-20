﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExtras : MonoBehaviour
{
    private void Awake()
    {
         GameObject[] objects = GameObject.FindGameObjectsWithTag("Music");
        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }
    }
}
