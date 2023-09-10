using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject virusShot;
    [SerializeField] EnemyInfo enemyInfo;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject player;

    private int attackRange;
    private int attackDamage;
    private float attackTimer = 0f;
    private Vector3 shotDirection;

    private CircleCollider2D attackField;

    private Transform playerTrans;
    private RaycastHit2D raycast;

    // Start is called before the first frame update
    void Start()
    {
        attackRange = enemyInfo.attackRange;
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
            // Check that attack timer is 0
            if (attackTimer < 1f)
            {
                Aim();
                if (isPlayerVisible())
                {
                    Shoot();
                    attackTimer = 2f;
                }
            }
        }
    }

    private void Shoot()
    {
        if (enemyInfo.enemy.type == EnemyType.Virus)
        {
            var shot = Instantiate(virusShot, firePoint.position, firePoint.rotation);
            var virusS = shot.GetComponent<VirusShot>();
            virusS.shotDirection = shotDirection;
            virusS.player = player;
            virusS.damage = attackDamage;
        } else if (enemyInfo.enemy.type == EnemyType.Bug)
        {
            Debug.Log("Bug attack");
        }
    }

    private void Aim()
    {
        // Get player Hitspot
        playerTrans = player.transform.GetChild(1).GetComponent<Transform>();
        shotDirection = -(transform.position - playerTrans.position).normalized;
        Debug.DrawRay(transform.position, shotDirection * attackRange, Color.red);
    }

    private bool isPlayerVisible()
    {
        int enemyLayerMask = LayerMask.GetMask("Enemy");
        int pickupLayerMask = LayerMask.GetMask("Pickups");

        int combinedLayerMask = enemyLayerMask | pickupLayerMask;
        raycast = Physics2D.Raycast(transform.position, shotDirection, Mathf.Infinity, ~combinedLayerMask);
        Debug.DrawRay(transform.position, shotDirection * Mathf.Infinity, Color.green);

        if (raycast.collider != null)
        {
            if (raycast.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
}
