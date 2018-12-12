using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;
using System;

public class RaycastManager_Fuse : MonoBehaviour
{
    private GameObject raycasted_obj;

    [Header("Raycast Length/Layer")]
    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    [SerializeField] private Image uiCrosshair;
    private bool isCrosshairActive;

    private InventoryController_Fuse fuseInv;

    private void Start()
    {
        fuseInv = GameObject.FindWithTag("FuseInventory").GetComponent<InventoryController_Fuse>();
    }


    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if(hit.collider.CompareTag("Fuse"))
            {
                raycasted_obj = hit.collider.gameObject;
                CrosshairActive();
                isCrosshairActive = true;

                if (Input.GetMouseButtonDown(0))
                {
                    fuseInv.UpdateFuseUI();
                    AudioManager_Fuse.instance.Play("PickupSFX");
                    raycasted_obj.SetActive(false);
                }
            }

            if (hit.collider.CompareTag("FuseBox"))
            {
                raycasted_obj = hit.collider.gameObject;
                CrosshairActive();
                isCrosshairActive = true;

                if (Input.GetMouseButtonDown(0))
                {
                    raycasted_obj.GetComponent<FuseController>().CheckFuseBox();
                }
            }
        }

        else
        {
            if (isCrosshairActive)
            {
                CrosshairNormal();
            }
        }
    }

    void CrosshairActive()
    {
        uiCrosshair.color = Color.red;
    }

    void CrosshairNormal()
    {
        uiCrosshair.color = Color.white;
        isCrosshairActive = false;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (eventData.selectedObject.gameObject.CompareTag("Fuse"))
        {
            fuseInv.UpdateFuseUI();
            AudioManager_Fuse.instance.Play("PickupSFX");
            eventData.selectedObject.gameObject.SetActive(false);
        }

        if (eventData.selectedObject.gameObject.CompareTag("Fuse"))
        {
            eventData.selectedObject.gameObject.GetComponent<FuseController>().CheckFuseBox();
        }
    }
}
