using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public AudioSource efxSource;
	public float lowPitchRange = .95f;
	public float highPitchRange = 1.05f;

	public AudioClip gameOver;
	public AudioClip failure;
	public AudioClip secBeep;
    public AudioClip start;
    public AudioClip win;
    public AudioClip raise;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayClip(AudioClip clip){
		efxSource.clip = clip;
		efxSource.Play ();
	}
}
