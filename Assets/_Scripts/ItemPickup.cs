using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] InputManager editor;
    public InventoryManager inventoryManager;
    private Item item;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Show the puzzle to the player

        if (collision.gameObject.CompareTag("Pickup"))
        {
            editor.OpenInputField(() =>
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
            });
        }
    }
}