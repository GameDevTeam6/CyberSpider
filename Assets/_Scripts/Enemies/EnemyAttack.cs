using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private int attackRange;
    private int attackDamage;

    [SerializeField] EnemyInfo enemyInfo;

    private CircleCollider2D attackField;

    [SerializeField] GameObject virusShot;
    [SerializeField] Transform firePoint;

    private float attackTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        attackRange = enemyInfo.attackRange;
        Debug.Log(attackRange);
        attackDamage = enemyInfo.attackDamage;

        attackField = GetComponent<CircleCollider2D>();
        attackField.radius = attackRange*2;
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
            //Debug.Log("Player inbound");
            if (attackTimer < 1f)
            {
                Shoot();
                Debug.Log("Shots fired");
                attackTimer = 2f;
            }
        }
        //if (col.gameObject.CompareTag("Enemy"))
        //{
        //    Debug.Log("Collided with enemy");
        //    int damage = col.gameObject.transform.GetChild(0).GetComponent<EnemyInfo>().enemy.attackDamage;
        //    gameObject.GetComponent<PlayerStats>().ChangeHealth(-damage);
        //}
    }

    private void Shoot()
    {
        var shot = Instantiate(virusShot, firePoint.position, firePoint.rotation);
    }
}
