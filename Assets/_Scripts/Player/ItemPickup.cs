using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ItemPickup : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public AudioClip bitcoinPickupSound;
    public AudioClip healthPickupSound;
    private AudioSource audioSource;
    [SerializeField] InputManager editor;

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

        // Initialize the audio source
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        // collision with pickup items
        if (col.gameObject.CompareTag("Pickup"))
        {
            // fetch type of item
            ActionType itemType = col.gameObject.GetComponent<ItemInfo>().item.actionType;

            ////////////////// BITCOIN TOKEN ///////////////////

            if (itemType == ActionType.Score)
            {
                // fetch reward value to be added to the score
                float val = col.gameObject.GetComponent<ItemInfo>().item.actionValue;
                // update score
                _score = gameObject.GetComponent<PlayerStats>().ChangeScore(val);
                Destroy(col.gameObject);

                // Play the bitcoin pickup sound
                audioSource.PlayOneShot(bitcoinPickupSound);
            }

            ////////////////// HEALTH TOKEN ///////////////////
            else if (itemType == ActionType.Health)
            {
                // fetch current health
                float currentHealth = gameObject.GetComponent<PlayerStats>().GetHealth();
                // fetch health value to be added
                float val = col.gameObject.GetComponent<ItemInfo>().item.actionValue;

                if (currentHealth < (100 - val))
                {
                    // add boost value to current health
                    _health = gameObject.GetComponent<PlayerStats>().ChangeHealth(val);

                    // Play the health pickup sound
                    audioSource.PlayOneShot(healthPickupSound);
                }
                else
                {
                    // restrict max health to 100
                    float diff = 100 - currentHealth;
                    _health = gameObject.GetComponent<PlayerStats>().ChangeHealth(diff);
                }

                Destroy(col.gameObject);
            }

            else
            {
                Item item = col.gameObject.GetComponent<ItemInfo>().item;
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
