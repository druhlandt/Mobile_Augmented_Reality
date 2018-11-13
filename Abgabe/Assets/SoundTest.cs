using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour {

	public AudioSource efxSource;
	public float lowPitchRange = .95f;
	public float highPitchRange = 1.05f;

	public AudioClip nextSound;
	public AudioClip backSound;
	public AudioClip sortSound;

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
