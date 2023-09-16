using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image image;
    public Color selectedCol, notSelectedCol;

    public void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedCol;
    }

    public void Deselect()
    {
        image.color = notSelectedCol;
    }
}
