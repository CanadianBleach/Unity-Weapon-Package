using System.Collections;
using System.Collections.Generic;
using static InterfaceUtils;
using static GunController;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    GameObject activeMenu;

    private bool inventoryOpen;

    public static InventoryController inventoryController;

    private void Start()
    {
        inventoryOpen = false;
        interfaceUtils.CloseInventory();

        inventoryController = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inventoryOpen)
        {
            // Close the inventory and enable player movement
            gunController.primaryGun.UpdateGunStats();
            gunController.secondaryGun.UpdateGunStats();
            interfaceUtils.CloseInventory();
            inventoryOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !inventoryOpen)
        {
            // Open the inventory and disable player movement
            interfaceUtils.OpenInventory();
            inventoryOpen = true;
        }
    }
}
