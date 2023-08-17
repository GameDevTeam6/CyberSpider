using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    [SerializeField] private GameObject inventoryPanel;

    //public Transform ItemContent;
    //public GameObject InventoryItem;

    private void Awake()
    {
        Instance = this; 
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    private void ItemToInventoryPanel()
    {
        foreach (var item in Items)
        {
            GameObject obj;

        }
    }

    //public void ListItems()
    //{
    //    foreach (var item in Items)
    //    {
    //        GameObject obj = Instantiate(InventoryItem, ItemContent);
    //        var itemName = item.itemName;
    //        var itemicon = item.Icon;
    //    }
    //}

    //[SerializeField] protected List<GameObject> inventory = new List<GameObject>();
    //private GameObject onHand;
    //private GameObject offHand;

    //private bool onHandEquipped = false;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    onHand = transform.Find("onHand").gameObject;
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Weapon"))
    //    {
    //        inventory.Add(collision.gameObject);
    //        if (!onHandEquipped)
    //        {
    //            EquipWeapon(collision.gameObject);
    //        }
    //        //collision.gameObject.SetActive(false);
    //    }
    //}

    //private void EquipWeapon(GameObject weapon)
    //{
    //    weapon.transform.parent = onHand.transform;
    //    onHandEquipped = true;
    //}
}
