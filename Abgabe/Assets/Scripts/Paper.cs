using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour {

    public float speed = 1;
    public GameObject endPos;

    private bool printed;

    private void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (!printed)
            transform.position = Vector3.Lerp(transform.position, endPos.transform.position, Time.deltaTime * speed);

        if (transform.position == endPos.transform.position)
            printed = true;
    }
}
