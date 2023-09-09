using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryManager inventoryManager;

    private Item item;
    //[SerializeField] InputManager editor;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        // if player collides with pickup items
        if (coll.gameObject.CompareTag("Pickup"))
        {
          //editor.OpenQuestionPanel(() => {
          //});

            Item item = coll.gameObject.GetComponent<ItemInfo>().item;
            if (item.actionType == ActionType.Score)
            {
              // delete token from scene
              Destroy(coll.gameObject);
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
