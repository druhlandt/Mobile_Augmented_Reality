using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FuseCollission : MonoBehaviour, IInputClickHandler
{
    public InventoryController_Fuse fuseInv;


    void Awake()
    {
        InputManager.Instance.AddGlobalListener(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        GetComponent<AudioSource>().Play();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        fuseInv.UpdateFuseUI();
        AudioManager_Fuse.instance.Play("PickupSFX");
        gameObject.SetActive(false);
    }
}
