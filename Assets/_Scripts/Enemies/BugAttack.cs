using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAttack : MonoBehaviour
{
    [SerializeField] EnemyInfo enemyInfo;
    [SerializeField] GameObject player;
    [SerializeField] Transform firePoint;

    private int attackRange;
    private int attackDamage;
    private float attackTimer = 0f;
    private Vector3 shotDirection;

    private CircleCollider2D attackField;
    
    // Start is called before the first frame update
    void Start()
    {
        attackRange = enemyInfo.attackRange;
        attackDamage = enemyInfo.attackDamage;

        attackField = GetComponent<CircleCollider2D>();
        attackField.radius = attackRange * 2;
    }

    void FixedUpdate()
    {
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // Check that attack timer is 0
            if (attackTimer < 1f)
            {
                int damage = col.gameObject.transform.GetChild(0).GetComponent<EnemyInfo>().enemy.attackDamage;
                player.GetComponent<PlayerStats>().ChangeHealth(-damage);
            }
        }
    }
}
