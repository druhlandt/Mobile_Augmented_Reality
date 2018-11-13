using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;

    #region GameObjects to control
    [SerializeField] public PinCodeControl doorlock;
    [SerializeField] public Box box;
    [SerializeField] public GameObject[] solvedPuzzles;
    #endregion

    #region Time configuration
    [SerializeField] public float playTime;
    [SerializeField] public int timeToDecrease;
    #endregion

    #region private fields
    [SerializeField] private float timer;
    [HideInInspector]
    [SerializeField] private int[] solution;
    #endregion

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
        MakeSolution();
        //VoiceOver Ansage 1
        SpawnBox();
        //Warten auf Box
        SpawnLock();
        //Warten auf Lock
        //VoiceOver Ansage 2
        
        StartTimer();
	}
	
	// Update is called once per frame
	void Update () {
        //CheckTimer();
        String actualTime = string.Format("{0}:{1}", TimeSpan.FromMilliseconds(timer).TotalMinutes, TimeSpan.FromMilliseconds(timer).TotalSeconds);
        doorlock.UpdateText(actualTime);

        if (Input.GetMouseButtonDown(0))        //TESTFUNKTION
        {
            print("click");
            StartNextPuzzle();
        
        }
	}

    private void StartTimer()
    {
        //Irgendwas das Daniel macht
    }

    private void GameOver()
    {
        //TODO GameOver Screen, Optionen zum Neustart
    }

    private void SpawnBox()
    {
        box.gameObject.SetActive(true);
    }

    private void SpawnLock()
    {
        doorlock.gameObject.SetActive(true);
        doorlock.SetCode(solution.ToString());
        StartTimer();
    }

    public void StartNextPuzzle()
    {
        if (!CheckWinCondition())
            box.SpawnNextPuzzle();
        else
            ;//WinScreen
    }

    private void DecreaseTimer()
    {
        timer -= timeToDecrease;
    }

    private void MakeSolution()
    {
        solution = new int[solvedPuzzles.Length];
        for(int i = 0; i < solution.Length; i++) 
            solution[i] = UnityEngine.Random.Range(0, 9);
    }

    private void CheckTimer()
    {
        if (timer <= 0)
            GameOver();
    }

    private void ShowNumber()
    {
        foreach(GameObject go in solvedPuzzles)
        {
            if (go.activeSelf)
            {
                int nextNumber = Array.FindIndex(solvedPuzzles, x => x.Equals(go));
                box.ShowNextNumber(solution[nextNumber]);
            }
        }
    }

    public void PuzzleFail()
    {
        //Failsound abspielen
        //Optisch verdeutlichen?

        DecreaseTimer();
    }
    
    private bool CheckWinCondition()
    {
        int solved = 0;
        foreach (GameObject go in solvedPuzzles)
            if (go.activeSelf)
                solved++;

        return solved == solvedPuzzles.Length;
    }
}
