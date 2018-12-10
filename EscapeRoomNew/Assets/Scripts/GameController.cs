using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class GameController : MonoBehaviour {
    public static GameController instance;

    #region GameObjects to control
    [SerializeField] public PinCodeControl doorlock;
    [SerializeField] public Box box;
    [SerializeField] public GameObject[] solvedPuzzles;
    #endregion

    #region Time configuration
    [SerializeField] public float startingTime = 900f;
    [SerializeField] public float timeToDecrease = 600f;
    #endregion

    #region private fields
    [HideInInspector]
    [SerializeField] private int[] solution;

    private float currentTime = 0f;
    private int tmpSec = 999;
    private bool bStart;
    private bool win;
    private SoundController sc;
    private bool pause;
    #endregion

    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

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
        //SpawnBox();
        //Warten auf Box
        //SpawnLock();
        //Warten auf Lock
        //VoiceOver Ansage 2

        //StartPauseTimer();
        sc = GetComponent<SoundController>();
        currentTime = startingTime;
        bStart = win = pause = false;

        keywords.Add("Next", () => {
            StartNextPuzzle();
        });

        keywords.Add("Pause", () => {
            if (!pause)
            {
                StartPauseTimer();
                pause = !pause;
            }
        });

        keywords.Add("Anti Pause", () => {
            if (pause)
            {
                StartPauseTimer();
                pause = !pause;
            }
        });
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordReconizeOnPhraseReconized;
        keywordRecognizer.Start();
    }

    void KeywordReconizeOnPhraseReconized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;

        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    // Update is called once per frame
    void Update () {
   
        win = CheckWinCondition();
        CheckTimer();

        if (Input.GetMouseButtonDown(0))        //TESTFUNKTION
        {
            print("click");
            StartNextPuzzle(); 
            sc.PlayClip(sc.failure);
        }
	}

    private string getTime()
    {
        float min = currentTime / 60;
        int tmp = (int)min;
        int sec = (int)((currentTime) % 60);
        if (sec != tmpSec && bStart)
        {
            sc.PlayClip(sc.secBeep);
            tmpSec = sec;
        }
        if (sec > 9)
        {
            return tmp + "min " + sec + "s";
        }
        else
        {
            return tmp + "min 0" + sec + "s";
        }
    }

    public void StartPauseTimer()
    {
        //doorlock.UpdateText(getTime());
        bStart = !bStart;
        sc.PlayClip(sc.start);
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
        StartPauseTimer();
    }

    public void StartNextPuzzle()//hiermit starten
    {
        if (!CheckWinCondition())
        {
            sc.PlayClip(sc.raise);
            box.SpawnNextPuzzle();
        }
        else
            ;//WinScreen
    }

    private void DecreaseTimer()
    {
        currentTime -= timeToDecrease;
        sc.PlayClip(sc.failure);
    }

    private void MakeSolution()
    {
        solution = new int[solvedPuzzles.Length];
        for(int i = 0; i < solution.Length; i++) 
            solution[i] = UnityEngine.Random.Range(0, 9);
    }

    private void CheckTimer()
    {
        if (bStart && !win)
        {
            if (currentTime >= 0)
            {
                currentTime -= 1 * Time.deltaTime;
                //doorlock.UpdateText(getTime());
            }
            else
            {
                //doorlock.UpdateText("Game Over");
                GameOver();
                sc.PlayClip(sc.gameOver);
            }
        }
        else
        {
            //Hier kann was mit gewiinen hin
        }
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
        //Optisch verdeutlichen?
        sc.PlayClip(sc.failure);
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
