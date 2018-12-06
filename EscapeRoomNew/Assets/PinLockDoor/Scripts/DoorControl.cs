using UnityEngine;
using System.Collections;

// DoorControl - moves the door parts according to commands
public class DoorControl : MonoBehaviour, ILockable {
	// door parts (GameObjects)
	public GameObject ASide;
	public GameObject BSide;

	// open and closed positions (where to slide the parts to)
	public Transform ASideOpenPos;
	public Transform BSideOpenPos;
	public Transform ASideClosedPos;
	public Transform BSideClosedPos;
	
	// helper variables
	public bool IsOpen;
	public float speed;

	// initialization
	void Start () {
		// door starts closed / locked
		IsOpen = false;			
	}
	
	// Update is called once per frame
	void Update () {
		// move it to goal position
		if (IsOpen) {
			ASide.transform.position = Vector3.Lerp(ASide.transform.position, ASideOpenPos.position, speed * Time.deltaTime);
			BSide.transform.position = Vector3.Lerp(BSide.transform.position, BSideOpenPos.position, speed * Time.deltaTime);
		} else {
			ASide.transform.position = Vector3.Lerp(ASide.transform.position, ASideClosedPos.position, speed * Time.deltaTime);
			BSide.transform.position = Vector3.Lerp(BSide.transform.position, BSideClosedPos.position, speed * Time.deltaTime);
		}
	}

	// Interface implementation - lock (or close)
	public void Lock(){
		// in this case, we close the door right away. it could be used to just set a lock / unlock state
		IsOpen = false;
	}

	// Interface implementation - unlock (or open)
	public void Unlock(){
		// same here - open the door right away for demo purposes
		IsOpen = true;
	}
}
