using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject playerEquipSlot;
    [SerializeField] private PlayerController playerController;
    
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject equippedItemPrefab;
    public int selectedSlot = -1;
    private int maxStackItems = 4;

    public bool itemEquipped = false;
    
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
                // Check that new slot is not empty
                if (CheckSlot(number))
                {
                    ChangeSelectedSlot(number - 1);
                } else
                {
                    return;
                }
            }
        }
    }

    public bool CheckSlot(int slot)
    {
        // Check that new slot is different from current slot
        if (slot - 1 == selectedSlot)
        {
            return false;
        }
        
        // Check that there is something in slot
        if (inventorySlots[slot - 1].gameObject.transform.childCount == 0)
        {
            return false;
        } else
        {
            return true;
        }
    }

    void ChangeSelectedSlot(int newSlot)
    {
        // Check if there is currently a selected slot, if so then deselect
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        // Set new selected slot
        inventorySlots[newSlot].Select();
        selectedSlot = newSlot;

        EquipItem(GetSelectedItem().item);
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
                //Debug.Log("Creating stack at " + i);
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
                //Debug.Log("Empty slot at " + i);
                SpawnNewItem(item, slot);
                //if (!itemEquipped)
                //{
                //    EquipItem(item);
                //}
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
        InventorySlot slot = inventorySlots[selectedSlot];
		InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        return itemInSlot;
    }

    // Method that creates a clone of the Item in the players equipped slot
    public void EquipItem(Item item)
    {
        UnequipItem();
        GameObject equippedItemGO = Instantiate(equippedItemPrefab, playerEquipSlot.transform);
        EquippedItem equippedItem = equippedItemGO.GetComponent<EquippedItem>();
        equippedItem.InitializeItem(item);
        //equippedItemGO.transform.position = Vector2.zero;
        itemEquipped = true;
        playerController.ItemEquipped();
    }

    // Method that destroys currently equipped item (if any)
    public void UnequipItem()
    {
        if (playerEquipSlot.transform.childCount > 0)
        {
            Destroy(playerEquipSlot.transform.GetChild(0).gameObject);
            itemEquipped = false;
            playerController.ItemUnequipped();
        }
    }

    public void DeselectCurrent()
    {
        inventorySlots[selectedSlot].Deselect();
        selectedSlot = -1;
    }
}
