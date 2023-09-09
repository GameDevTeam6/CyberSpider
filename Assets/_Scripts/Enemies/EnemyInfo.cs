using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] FloatingHealthBar healthBar;
    public GameObject player;
    public Enemy enemy;

    private float maxHealth;
    private float currentHealth;
    public int attackRange;
    public int attackDamage;

    void Awake()
    {
        maxHealth = enemy.health;
        currentHealth = maxHealth;

        attackRange = enemy.attackRange;
        attackDamage = enemy.attackDamage;
    }

    private void Start()
    {
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
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
        Destroy(transform.parent.gameObject);
    }
}
