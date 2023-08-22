using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    private int maxStackItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject equippedItemPrefab;
    private int selectedSlot = -1;

    [SerializeField] private GameObject playerEquipSlot;
    
    private void Start()
    {
        //ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 5)
            {
                Debug.Log(number);
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    void ChangeSelectedSlot(int newSlot)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newSlot].Select();
        selectedSlot = newSlot;
        try
        {
            EquipItem(GetSelectedItem().item);
        } catch (Exception e)
        {
            Debug.Log("No item in slot: " + e.Message);
        }
    }

    public bool AddItem(Item item)
    {
        // Check if inv already has same item type
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && 
                itemInSlot.item == item && 
                itemInSlot.count < maxStackItems && 
                itemInSlot.item.stackable)
            {
                Debug.Log("Creating stack at " + i);
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // Check for open inv slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                Debug.Log("Empty slot at " + i);
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public InventoryItem GetSelectedItem()
    {
        if (selectedSlot < 0) 
        {
            Debug.Log("No slot selected");
            return null;
        } 
        InventorySlot slot = inventorySlots[selectedSlot];
		InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
		if (itemInSlot == null)
		{
            Debug.Log("No item in selected slot");
		    return null;
		} else {
			return itemInSlot;
        }
    }

    // Method that creates a clone of the Item in the players equipped slot
    public void EquipItem(Item item)
    {
        UnequipItem();

        if (item != null)
        {
            GameObject equippedItemGO = Instantiate(equippedItemPrefab, playerEquipSlot.transform);
            EquippedItem equippedItem = equippedItemGO.GetComponent<EquippedItem>();
            equippedItem.InitializeItem(item);
        } else
        {
            return;
        }
    }

    // Method that destroys currently equipped item (if any)
    public void UnequipItem()
    {
        if (playerEquipSlot.transform.childCount > 0)
        {
            Destroy(playerEquipSlot.transform.GetChild(0).gameObject);
        }
    }
}
