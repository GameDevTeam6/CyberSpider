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
        // if player collides with pickup items
        if (coll.gameObject.CompareTag("Pickup"))
        {
            // fetch iten
            item = coll.gameObject.GetComponent<ItemInfo>().item;

            // if the pickup item is a bitcoin token
            if (item.actionType == ActionType.Score)
            {
                // delete token from scene
                Destroy(coll.gameObject);

                //Debug.Log("Picked up a bitcoin!");
                //Debug.Log("New Score:" + gameObject.GetComponent<PlayerStats>().GetScore());
            }
            // if the pickup item is a health token
            else if (item.actionType == ActionType.Health)
            {
                // delete token from scene
                Destroy(coll.gameObject);
            }
            // if any other item picked up
            else
            {
                bool result = inventoryManager.AddItem(item);
                // if there are available inventory slots
                if (result)
                {
                    Debug.Log("Item added to inventory");
                    // hide from scene
                    coll.gameObject.SetActive(false);                   
                }
            }
        }
    }
}
