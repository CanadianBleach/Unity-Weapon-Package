using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotOutline : MonoBehaviour
{
    public Color highlightColor;
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnMouseOver()
    {
        image.color = highlightColor;
    }

    public void OnMouseExit()
    {
        image.color = Color.white;
    }
}
