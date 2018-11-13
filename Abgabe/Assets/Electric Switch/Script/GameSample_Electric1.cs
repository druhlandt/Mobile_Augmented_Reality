using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSample_Electric1 : MonoBehaviour
{
    public Electric1 electric1;

    public GameObject showUI;

    void Start ()
    {
        electric1.onComplete += OnComplete;
    }

    private void OnComplete()
    {
        showUI.SetActive(true);
        electric1.gamePause = true;
    }

    public void CloseUI ()
    {
        showUI.SetActive(false);
        electric1.gamePause = false;
        electric1.Setup();
    }
}
