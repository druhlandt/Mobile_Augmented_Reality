using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSample : MonoBehaviour
{
    public ButtonPuzzle buttonPuzzle;

    public GameObject showUI;

    void Start ()
    {
        buttonPuzzle.onComplete += OnComplete;
    }

    private void OnComplete()
    {
        showUI.SetActive(true);
        buttonPuzzle.gamePause = true;
    }

    public void CloseUI ()
    {
        showUI.SetActive(false);
        buttonPuzzle.gamePause = false;
        buttonPuzzle.Setup();
    }
}
