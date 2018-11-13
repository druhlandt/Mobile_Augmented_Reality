using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

	float currentTime = 0f;
	float startingTime = 900f;
	
	private bool bStart;
	private bool win;
	
	[SerializeField] Text countdownText;

	// Use this for initialization
	void Start() {
		currentTime = startingTime;
		countdownText.text = getTime();	
		bStart = win = false;
	}
	
	private string getTime(){
		float min = currentTime / 60;
		int tmp = (int)min;
		float sec = (int)((currentTime) % 60);
        if (sec > 9){
            return tmp + "min " + sec + "s";
        }else{
            return tmp + "min 0" + sec + "s";
        }
	}
	
	public void run() {
		bStart = !bStart;
	}
	
	public void raiseCurrentTime() {
		currentTime += 10;
	}
	
	public void degradeCurrentTime() {
		currentTime -= 10;
	}
	
	public void ready() {
		win = true;
		countdownText.text = "Win";
	}
	
	// Update is called once per frame
	void Update() {
		if(bStart && !win){
			if (currentTime >= 0){
				currentTime -= 1 * Time.deltaTime;
				countdownText.text = getTime();	
			} else{
				countdownText.text = "Game Over";
			}
		}
	}
}
