using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public ItemType type;
    public ActionType actionType;
    public float actionRange = 0;
    public float actionValue = 0;
    public bool stackable = false;
    public bool consumable = false;
}

public enum ItemType
{
    Powerup,
    Weapon
}

public enum ActionType
{
    Health,
    Speed,
    Damage
}
