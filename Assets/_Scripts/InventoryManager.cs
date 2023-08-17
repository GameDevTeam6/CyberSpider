using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] protected List<GameObject> inventory = new List<GameObject>();
    private GameObject onHand;
    private GameObject offHand;

    private bool onHandEquipped = false;

    // Start is called before the first frame update
    void Start()
    {
        onHand = transform.Find("onHand").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            inventory.Add(collision.gameObject);
            if (!onHandEquipped)
            {
                EquipWeapon(collision.gameObject);
            }
            //collision.gameObject.SetActive(false);
        }
    }

    private void EquipWeapon(GameObject weapon)
    {
        weapon.transform.parent = onHand.transform;
        onHandEquipped = true;
    }
}
