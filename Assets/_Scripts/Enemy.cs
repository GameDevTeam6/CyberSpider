using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Enemy")]
public class Enemy : ScriptableObject
{
    public Sprite image;
    public EnemyType type;
    public int attackRange = 0;
    public int attackDamage = 0;
}

public enum EnemyType
{
    Bug,
    Virus
}