using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButtonObject_Electric1 : MonoBehaviour
{
    public int clickIndex = 0;
    public Electric1 electric1;

    private float num = -90;

    private void Start()
    {
    }

    void OnMouseDown()
    {
        electric1.ClickButton(clickIndex);
    }

    private void Update()
    {
        if (electric1.buttonPressed[clickIndex] == 0)
        {
            num += (-90 - num) / 2;
        }
        else
        {
            num += (0 - num) / 2;
        }
        transform.localRotation = Quaternion.Euler(num, 0, 0);
    }
}
