using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.Buttons;

public class FuseCollission : MonoBehaviour
{
    
    public void OnCollisionEnter(Collision collision)
    {
        GetComponent<AudioSource>().Play();
    }
    
}
