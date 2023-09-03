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
