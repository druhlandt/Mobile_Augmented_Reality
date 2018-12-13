using UnityEngine;
using System.Collections;
using HoloToolkit.Unity.InputModule;
using System;

public class FuseController : MonoBehaviour
{
    #region Fuse Controlls
    [SerializeField] public FusePuzzle fusePuzzle;
    #endregion

    #region Fuse Booleans
    [Header("Fuse Booleans")]
    [SerializeField] private bool fuse1Bool;
    [SerializeField] private bool fuse2Bool;
    [SerializeField] private bool fuse3Bool;
    [SerializeField] private bool fuse4Bool;

    [SerializeField] private bool powerOn;
    #endregion

    #region Fuse Main Objects
    [Header("Fuse Main Objects")]
    [SerializeField] private GameObject fuseObject1;
    [SerializeField] private GameObject fuseObject2;
    [SerializeField] private GameObject fuseObject3;
    [SerializeField] private GameObject fuseObject4;
    #endregion

    #region Fuse Lights
    [Header("Fuse Lights")]
    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;
    [SerializeField] private GameObject light3;
    [SerializeField] private GameObject light4;
    #endregion

    #region Materials
    [Header("Materials")]
    [SerializeField] private Material greenButton;
    [SerializeField] private Material redButton;
    #endregion

    [SerializeField] private InventoryController_Fuse fuseInv;

    void Start()
    {
        #region Set Light Colour/Fuse Objects, if any fuses booleans are currently set
        if (fuse1Bool)
        {
            light1.GetComponent<Renderer>().material = greenButton;
            fuseObject1.SetActive(true);
        }

        if (fuse2Bool)
        {
            light2.GetComponent<Renderer>().material = greenButton;
            fuseObject2.SetActive(true);
        }

        if (fuse3Bool)
        {
            light3.GetComponent<Renderer>().material = greenButton;
            fuseObject3.SetActive(true);
        }

        if (fuse4Bool)
        {
            light4.GetComponent<Renderer>().material = greenButton;
            fuseObject4.SetActive(true);
        }
        #endregion

        fuseInv = GameObject.FindWithTag("FuseInventory").GetComponent<InventoryController_Fuse>();
    }

    void PoweredUp()
    {
        //DO YOUR DOOR UNLOCKING OR WHATEVER RESULT OF THE FUSE SYSTEM HAVING BEEN COMPLETED!
        fusePuzzle.PowerOn();
    }

    public void CheckFuseBox()
    {
        #region No Fuses Check
        if (fuseInv.inventoryFuses <= 0 && !powerOn)
        {
            AudioManager_Fuse.instance.Play("ZapSFX");
        }
        #endregion

        if (fuseInv.inventoryFuses >= 1)
        {
            #region Fuse Check 1
            if (!fuse1Bool)
            {
                fuseObject1.SetActive(true);
                light1.GetComponent<Renderer>().material = greenButton;
                fuseInv.MinusFuseUI();
                fuse1Bool = true;
                AudioManager_Fuse.instance.Play("ZapSFX");
                FusesEngaged();
            }
            #endregion 

            #region Fuse Check 2
            else if (!fuse2Bool)
            {
                fuseObject2.SetActive(true);
                light2.GetComponent<Renderer>().material = greenButton;
                fuseInv.MinusFuseUI();
                fuse2Bool = true;
                AudioManager_Fuse.instance.Play("ZapSFX");
                FusesEngaged();
            }
            #endregion

            #region Fuse Check 3
            else if (!fuse3Bool)
            {
                fuseObject3.SetActive(true);
                light3.GetComponent<Renderer>().material = greenButton;
                fuseInv.MinusFuseUI();
                fuse3Bool = true;
                AudioManager_Fuse.instance.Play("ZapSFX");
                FusesEngaged();
            }
            #endregion

            #region Fuse Check 4
            else if (!fuse4Bool)
            {
                fuseObject4.SetActive(true);
                light4.GetComponent<Renderer>().material = greenButton;
                fuseInv.MinusFuseUI();
                fuse4Bool = true;
                AudioManager_Fuse.instance.Play("ZapSFX");
                FusesEngaged();
            }
            #endregion
        }
    }

    void FusesEngaged()
    {
        #region FusesEngaged Section

        if (fuse1Bool && fuse2Bool && fuse3Bool && fuse4Bool)
        {
            powerOn = true;
            GetComponent<AudioSource>().Play();
            PoweredUp();
        }

        #endregion
    }

}
