using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseCollission : MonoBehaviour {

    public void OnCollisionEnter(Collision collision)
    {
        GetComponent<AudioSource>().Play();
    }
}
