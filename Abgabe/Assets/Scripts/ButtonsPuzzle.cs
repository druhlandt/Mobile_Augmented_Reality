using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsPuzzle : Puzzle {
    public ButtonPuzzle buttonPuzzle;

    private bool isEndPos;
    private bool isOpen;

    // Use this for initialization
    void Start () {
        buttonPuzzle.onComplete += OnComplete;
        StartCoroutine(OpenButtonPuzzle());
    }

    public void OnComplete()
    {
        PuzzleSolved();
    }

    IEnumerator OpenButtonPuzzle()
    {
        Box.instance.GetComponent<Animation>().Play("OpenButtons");
        Box.instance.GetComponent<AudioSource>().clip = openSound;
        Box.instance.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        buttonPuzzle.gameObject.SetActive(true);
    }

    
}
