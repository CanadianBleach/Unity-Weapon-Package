using System.Collections;
using static InterfaceUtils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour, IDropHandler, IDragHandler, IEndDragHandler
{
    private Transform parent;
    public InventorySlot slot;
    private Image img;
    public Sprite activeItemImage;
    public Color invisibleColour;
    public Color visibleColour;

    void Awake()
    {
        parent = transform.parent;
        slot = GetComponentInParent<InventorySlot>();
        img = GetComponent<Image>();
        activeItemImage = img.sprite;
    }

    public void OnDrop(PointerEventData eventData)
    {

        Sprite tmpSprite = activeItemImage;

        if (interfaceUtils.mouseOnSlot != null)
        {
            img.color = invisibleColour;
            slot.attachmentImage.sprite = null;
            activeItemImage = null;
            StartCoroutine(delayNewPlacement(tmpSprite));
        }
    }

    IEnumerator delayNewPlacement(Sprite newSprite)
    {
        yield return new WaitForSeconds(0.1f);

        //what happens if we drag and drop an item on to a slot with an item already in it?
        //we need to take note of what is in the slot being dragged, and the slot that gets dropped.
        //we then need to use temporary variables to ensure that each slot swaps information with one another.

        //ensure the mouse is over a slot.
        if (interfaceUtils.mouseOnSlot != null)
        {
            if (interfaceUtils.mouseOnSlot == slot)
            {
                slot.attachmentImage.sprite = newSprite;
                activeItemImage = newSprite;
                img.color = visibleColour;
                
            } else
            {

                //lets make sure there is not already anything assigned to the slot we are over.
                if (interfaceUtils.mouseOnSlot.attachmentImage.sprite != null)
                {
                    //the new slot already has something inside it...
                    Debug.Log("The new slot already has something in it (switching)");
                    Sprite SpriteBackup = interfaceUtils.mouseOnSlot.attachmentImage.sprite;
                    interfaceUtils.mouseOnSlot.attachmentImage.sprite = newSprite;
                    interfaceUtils.mouseOnSlot.attachmentImage.color = visibleColour;
                    interfaceUtils.mouseOnSlot.attachmentImage.gameObject.SetActive(true);
                    interfaceUtils.mouseOnSlot.gameObject.GetComponentInChildren<ItemHandler>().activeItemImage = newSprite;
                    activeItemImage = SpriteBackup;
                    slot.attachmentImage.sprite = SpriteBackup;
                    img.color = visibleColour;
                }
                else
                {
                    //the new slot is empty.
                    Debug.Log("The new slot is empty.");
                    interfaceUtils.mouseOnSlot.attachmentImage.sprite = newSprite;
                    interfaceUtils.mouseOnSlot.attachmentImage.color = visibleColour;
                    interfaceUtils.mouseOnSlot.attachmentImage.gameObject.SetActive(true);
                    interfaceUtils.mouseOnSlot.gameObject.GetComponentInChildren<ItemHandler>().activeItemImage = newSprite;
                    img.color = invisibleColour;
                    activeItemImage = null;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        transform.parent = interfaceUtils.dragParent.transform;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
    }
}
