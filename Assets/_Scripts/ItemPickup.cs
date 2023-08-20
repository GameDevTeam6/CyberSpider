using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public InventoryManager inventoryManager;
    [SerializeField] InputManager editor;
    //public Item[] itemsToPickup;

    private Item item;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Show the puzzle to the player

        if (collision.gameObject.CompareTag("Pickup"))
        {
            editor.OpenInputField(() => {
                Item item = collision.gameObject.GetComponent<ItemInfo>().item;
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
            });
        }
    }

}
