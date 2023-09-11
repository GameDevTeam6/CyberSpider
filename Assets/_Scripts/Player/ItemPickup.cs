using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryManager inventoryManager;

    private Item item;
    //[SerializeField] InputManager editor;

    //public float _speed;
    public float _score;
    public float _health;
    public float _time;

    private void Start()
    {
        // fatch initial player stats values
        //_speed = gameObject.GetComponent<PlayerStats>().GetSpeed();
        _health = gameObject.GetComponent<PlayerStats>().GetHealth();
        _score = gameObject.GetComponent<PlayerStats>().GetScore();
        _time = gameObject.GetComponent<PlayerStats>().GetTime();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("ItemPickup::OnTrigger, collided with:" + col.gameObject.tag);

        // collision with pickup items
        if (col.gameObject.CompareTag("Pickup"))
        {
            // fetch type of item
            ActionType itemType = col.gameObject.GetComponent<ItemInfo>().item.actionType;

            ////////////////// BITCOIN TOKEN ///////////////////

            Debug.Log("PlayerController::OnCollision, picked up:" + itemType);

            if (itemType == ActionType.Score)
            {
                // fetch reward value to be added to the score
                float val = col.gameObject.GetComponent<ItemInfo>().item.actionValue;
                // update score
                _score = gameObject.GetComponent<PlayerStats>().ChangeScore(val);
            }

            ////////////////// HEALTH TOKEN ///////////////////
            if (itemType == ActionType.Health)
            {
                // fetch current health
                float currentHealth = gameObject.GetComponent<PlayerStats>().GetHealth();
                // fetch health value to be added
                float val = col.gameObject.GetComponent<ItemInfo>().item.actionValue;

                if (currentHealth < (100 - val))
                {
                    // add boost value to current health
                    _health = gameObject.GetComponent<PlayerStats>().ChangeHealth(val);
                }
                else
                {
                    // restrict max health to 100
                    float diff = 100 - currentHealth;
                    _health = gameObject.GetComponent<PlayerStats>().ChangeHealth(diff);
                }
            }
        }

        // if player collides with pickup items
        if (col.gameObject.CompareTag("Pickup"))
        {
          //editor.OpenQuestionPanel(() => {
          //});

            Item item = col.gameObject.GetComponent<ItemInfo>().item;
            if (item.actionType == ActionType.Score)
            {
                // delete token from scene
                Debug.Log("ItemPickup::OnTrigger, score item detected");
                Debug.Log(PlayerStats.finalScore);
                Destroy(col.gameObject);
            }
            // if the pickup item is a health token
            else if (item.actionType == ActionType.Health)
            {
              // delete token from scene
              Destroy(col.gameObject);
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
                    col.gameObject.SetActive(false);
                }
            }

        }
    }
}


