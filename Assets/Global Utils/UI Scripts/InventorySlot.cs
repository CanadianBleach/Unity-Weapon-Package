using System.Collections;
using System.Collections.Generic;
using static InterfaceUtils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public TextMeshProUGUI attachName;
    public Image attachmentImage;
    public Attachment attachment;
    public bool isEmpty = false;

    public void MouseEntered()
    {
        Debug.Log("Mouse Over Slot");
        interfaceUtils.mouseOnSlot = this;
    }

}
