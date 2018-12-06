using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController_Fuse : MonoBehaviour
{
    [SerializeField] private Text fuseUI;
    public int inventoryFuses;

    public void UpdateFuseUI()
    {
        inventoryFuses++;
        fuseUI.text = inventoryFuses.ToString("0");
    }

    public void MinusFuseUI()
    {
        inventoryFuses--;
        fuseUI.text = inventoryFuses.ToString("0");
    }
}
