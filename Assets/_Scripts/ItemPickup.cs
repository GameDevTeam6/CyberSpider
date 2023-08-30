using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryManager inventoryManager;

    private Item item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Pickup"))
        {
            item = coll.gameObject.GetComponent<ItemInfo>().item;
            bool result = inventoryManager.AddItem(item);
            if (result)
            {
                coll.gameObject.SetActive(false);
                Debug.Log("Item added to inventory");
            }
            else
            {
                Debug.Log("Inventory full!");
            }
        }
    }
}
