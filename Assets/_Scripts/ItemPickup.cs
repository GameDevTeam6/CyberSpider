using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public GameObject obj;
    public InventoryManager inventoryManager;
    //public Item[] itemsToPickup;

    private Item item;

    private void Start()
    {
        inventoryManager = obj.GetComponent<InventoryManager>();
        item = GetComponent<ItemInfo>().item;
    }

    //public void PickupItem(int id)
    //{
    //    inventoryManager.AddItem(itemsToPickup[id]);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inventoryManager.AddItem(item);
        }
    }
}
