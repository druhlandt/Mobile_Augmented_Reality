using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

	float currentTime = 0f;
	float startingTime = 900f;

    private int tmpSec = 999;
	private bool bStart;
	private bool win;
    private SoundController sc;

    [SerializeField] Text countdownText;
    public PinCodeControl plp;
    private PinCodeControl pcc;

    // Use this for initialization
    void Start() {
        pcc = plp.GetComponent <PinCodeControl>();
        sc = GetComponent<SoundController>();
        currentTime = startingTime;
        pcc.GetComponent<PinCodeControl>().UpdateText(getTime());
        bStart = win = false;
	}
	
	private string getTime(){
		float min = currentTime / 60;
		int tmp = (int)min;
		int sec = (int)((currentTime) % 60);
        if (sec != tmpSec && bStart){
            sc.PlayClip(sc.secBeep);
            tmpSec = sec;
        }
        if (sec > 9){
            return tmp + "min " + sec + "s";
        }else{
            return tmp + "min 0" + sec + "s";
        }
	}
	
	public void run() {
		bStart = !bStart;
        sc.PlayClip(sc.start);
    }
	
	public void raiseCurrentTime() {
		currentTime += 10;
        sc.PlayClip(sc.raise);
    }
	
	public void degradeCurrentTime() {
		currentTime -= 10;
        sc.PlayClip(sc.failure);
    }

    public void ready() {
		win = true;
        sc.PlayClip(sc.win);
        countdownText.text = "Win";
	}
	
	// Update is called once per frame
	void Update() {
		if(bStart && !win){
			if (currentTime >= 0){
				currentTime -= 1 * Time.deltaTime;
                pcc.UpdateText(getTime());
                print(pcc.getText());
            } else{
                pcc.UpdateText("Game Over");
                sc.PlayClip(sc.gameOver);
            }
		}
	}
}
