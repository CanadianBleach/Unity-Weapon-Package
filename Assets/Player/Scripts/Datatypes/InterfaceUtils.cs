using System.Collections;
using System.Collections.Generic;
using static PlayerController;
using static GunController;
using static InventoryController;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class InterfaceUtils : MonoBehaviour
{
    public GameObject playerUI;
    public GameObject inventoryUI;

    public GameObject dragParent;
    public InventorySlot mouseOnSlot;

    public Volume cameraVolume;
    public VolumeProfile menuBlurProfile;
    private VolumeProfile basicProfile;

    // References to the image objects in the inventory ui
    public Image primaryGunImage;
    public Image secondaryGunImage;

    // Reference to the imageSlots
    public InventorySlot[] primaryInventorySlots;
    public InventorySlot[] secondaryInventorySlots;

    public static InterfaceUtils interfaceUtils;

    private void Start()
    {
        interfaceUtils = this;
    }

    public void OpenInventory()
    {
        basicProfile = cameraVolume.profile;
        cameraVolume.profile = menuBlurProfile;
        SetInventoryImages();
        DisablePlayerUI();
        inventoryUI.SetActive(true);
        playerController.DisableMovement();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseInventory()
    {
        cameraVolume.profile = basicProfile;
        EnablePlayerUI();
        inventoryUI.SetActive(false);
        playerController.EnableMovement();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DisablePlayerUI()
    {
        playerUI.SetActive(false);
    }

    public void EnablePlayerUI()
    {
        playerUI.SetActive(true);
    }

    public void SetInventoryImages()
    {
        // Sets the images in the inventories
        primaryGunImage.sprite = gunController.primaryGun.gunImage;
        secondaryGunImage.sprite = gunController.secondaryGun.gunImage;

        SetAttachmentImages();
    }

    public void SetAttachmentImages()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i >= gunController.primaryGun.attachments.Length)
            {
                primaryInventorySlots[i].attachName.text = "Empty";
                primaryInventorySlots[i].attachmentImage.sprite = null;
                primaryInventorySlots[i].attachmentImage.gameObject.SetActive(false);
            }
            else
            {
                primaryInventorySlots[i].attachmentImage.gameObject.SetActive(true);
                primaryInventorySlots[i].attachmentImage.sprite = gunController.primaryGun.attachments[i].attachmentImage;
                primaryInventorySlots[i].attachment = gunController.primaryGun.attachments[i];
                primaryInventorySlots[i].attachName.text = gunController.primaryGun.attachments[i].name;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (i >= gunController.secondaryGun.attachments.Length)
            {
                secondaryInventorySlots[i].attachName.text = "Empty";
                secondaryInventorySlots[i].attachmentImage.sprite = null;
                secondaryInventorySlots[i].attachmentImage.gameObject.SetActive(false);
            }
            else
            {
                secondaryInventorySlots[i].attachmentImage.gameObject.SetActive(true);
                secondaryInventorySlots[i].attachmentImage.sprite = gunController.secondaryGun.attachments[i].attachmentImage;
                secondaryInventorySlots[i].attachment = gunController.secondaryGun.attachments[i];
                secondaryInventorySlots[i].attachName.text = gunController.secondaryGun.attachments[i].name;
            }
        }
    }
}
