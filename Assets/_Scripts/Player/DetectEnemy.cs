using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Collided with enemy");
            //int damage = col.gameObject.transform.GetChild(0).GetComponent<EnemyInfo>().enemy.attackDamage;
            //gameObject.GetComponent<PlayerStats>().ChangeHealth(-damage);
        }
    }
}
