using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMethoden : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NextCalled (){
		print ("NEXT");
	}

	public void BackCalled (){
		print ("BACK");
	}
	
	public void SortCalled (){
		print ("SORT");
	}
	
	public void StartCalled (){
		print ("START");
	}
	
	public void PauseCalled (){
		print ("Pause");
	}
	
	public void PlusCalled (){
		print ("Plus");
	}
	
	public void MinusCalled (){
		print ("Minus");
	}
	
	public void FertigCalled (){
		print ("Fertig");
	}
	
}
