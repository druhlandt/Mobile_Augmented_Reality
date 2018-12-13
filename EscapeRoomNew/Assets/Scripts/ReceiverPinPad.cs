using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.Receivers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverPinPad : InteractionReceiver
{

    protected override void InputDown(GameObject obj, InputEventData eventData)
    {
        switch (obj.tag)
        {
            case "PinLockButton":
                obj.GetComponent<ButtonAction>().OnMouseDown();
                break;

            default:
                break;
        }
    }
}
