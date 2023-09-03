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
        // if player collides with pickup items
        if (coll.gameObject.CompareTag("Pickup"))
        {
            item = coll.gameObject.GetComponent<ItemInfo>().item;

            // if pikcup item is score-related (bitcoin)
            if (item.actionType == ActionType.Score)
            {
                // remove from scene
                Destroy(coll.gameObject);

                Debug.Log("Picked up a bitcoin!");
                Debug.Log("New Score:" + gameObject.GetComponent<PlayerStats>().GetScore());
            }
            // if pickup item is health-related (heart)
            //else if (item.actionType == ActionType.Health)
            //{
                // hide from scene
                //coll.gameObject.SetActive(false);
                // **************************************
                // TO DO
                // **************************************
                //Debug.Log("Add heart to left hand side");
            //}
            // if pickup item is action-related
            else
            {
                bool result = inventoryManager.AddItem(item);
                // add to inventory (if there are available slots)
                if (result)
                {
                    Debug.Log("Item added to inventory");
                    // remove from scene
                    coll.gameObject.SetActive(false);                   
                }
            }
        }
    }
}
