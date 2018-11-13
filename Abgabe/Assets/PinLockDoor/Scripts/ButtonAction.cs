using UnityEngine;
using System.Collections;

// attach to button object that has a collider
public class ButtonAction : MonoBehaviour {
	public string ButtonValue;					// what key or string should be sent to the codecontroller
	public PinCodeControl myCodeController;		// the codeController to use
	private AudioSource myButtonSound;

	void Start(){
		// get the AudioSource comp to play sound
		myButtonSound = gameObject.GetComponent<AudioSource> ();
	}

	void OnMouseDown() {
		// send my input to the code controller
		myCodeController.addKeyInput (ButtonValue);
		myButtonSound.Play ();
	}
}
