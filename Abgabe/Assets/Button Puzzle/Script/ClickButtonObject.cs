using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButtonObject : MonoBehaviour
{
    public int clickIndex = 0;
    public ButtonPuzzle buttonPuzzle;

    void OnMouseDown()
    {
        buttonPuzzle.ClickButton(clickIndex);
    }
}
