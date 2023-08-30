using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryManager inventoryManager;
    //public Item[] itemsToPickup;

    private Item item;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            item = collision.gameObject.GetComponent<ItemInfo>().item;
            bool result = inventoryManager.AddItem(item);
            if (result)
            {
                collision.gameObject.SetActive(false);
                Debug.Log("Item added to inventory");
            }
            else
            {
                Debug.Log("Inventory full!");
            }
        }
    }
}
