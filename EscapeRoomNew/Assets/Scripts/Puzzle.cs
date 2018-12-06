using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {
    
    public AudioClip openSound;
    public GameObject solved;

    private void PuzzleFail()
    {
        GameController.instance.PuzzleFail();
    }

    public void PuzzleSolved()
    {
        solved.SetActive(true);
        GameController.instance.StartNextPuzzle();
    }
}
