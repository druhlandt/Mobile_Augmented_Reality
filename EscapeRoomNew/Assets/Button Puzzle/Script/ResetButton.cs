using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public ButtonPuzzle buttonPuzzle;
    private GameObject gc;

    void Start() {
        gc = GameObject.FindGameObjectWithTag("GameController");
    }

    public void OnMouseDown()
    {
        gc.GetComponent<GameController>().PuzzleFail();
        buttonPuzzle.Setup();
    }
}
