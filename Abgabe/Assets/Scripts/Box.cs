using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {
    public static Box instance;
    public GameObject[] puzzles;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);
            
        DontDestroyOnLoad(gameObject);
    }

        // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnNextPuzzle()
    {
        foreach (GameObject go in puzzles)
            if (!go.activeSelf)
            {
                go.SetActive(true);
                return;
            }
    }

    public void ShowNextNumber(int num)
    {
        //irgendeinem Objekt unter der Box die Nummer zuweisen
    }
}
