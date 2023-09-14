using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EquippedItem : MonoBehaviour
{
    [HideInInspector] public Item item;

    [Header("UI")]
    public SpriteRenderer spriteR;

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        spriteR.sprite = newItem.image;
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        //transform.Translate(new Vector3(0f, 0.06f, -1f));
        //transform.position = Vector2.zero;
    }

}
