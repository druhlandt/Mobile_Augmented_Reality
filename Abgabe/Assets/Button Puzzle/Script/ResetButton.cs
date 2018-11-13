using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public ButtonPuzzle buttonPuzzle;

    void OnMouseDown()
    {
        buttonPuzzle.Setup();
    }
}
