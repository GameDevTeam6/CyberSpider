using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] FloatingHealthBar healthBar;
    public Enemy enemy;

    private int maxHealth;
    private int currentHealth;

    private void Start()
    {
        maxHealth = enemy.health;
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {

    }
}
