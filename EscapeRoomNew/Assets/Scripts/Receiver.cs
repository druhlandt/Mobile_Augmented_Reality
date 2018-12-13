using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.Receivers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : InteractionReceiver
{
    public InventoryController_Fuse fuseInv;

    protected override void InputDown(GameObject obj, InputEventData eventData)
    {
        switch (obj.tag)
        {
            case "Fuse":
                fuseInv.UpdateFuseUI();
                AudioManager_Fuse.instance.Play("PickupSFX");
                obj.SetActive(false);
                break;

            case "FuseBox":
                GameObject.FindGameObjectWithTag("FuseBox").GetComponent<FuseController>().CheckFuseBox();
                break;

            case "Switch":
                obj.GetComponent<ClickButtonObject_Electric1>().OnMouseDown();
                break;

            case "ButtonBoard":
                obj.GetComponent<ClickButtonObject>().OnMouseDown();
                break;

            case "ButtonBoardReset":
                obj.GetComponent<ResetButton>().OnMouseDown();
                break;

            case "ElectricWave":
                obj.GetComponent<ElectricWave_ClickObjectWheel>().OnMouseDown();
                break;


            default:
                break;
        }
    }

    protected override void InputUp(GameObject obj, InputEventData eventData)
    {
        switch (obj.tag)
        {
            case "ElectricWave":
                obj.GetComponent<ElectricWave_ClickObjectWheel>().OnMouseUp();
                break;

            default:
                break;
        }
    }

    protected override void FocusEnter(GameObject obj, PointerSpecificEventData eventData)
    {
        switch (obj.tag)
        {
            case "ElectricWave":
                obj.GetComponent<ElectricWave_ClickObjectWheel>().OnFocusEnter();
                break;

            default:
                break;
        }
    }
}
