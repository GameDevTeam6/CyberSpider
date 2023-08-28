using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    private Item equippedItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && gameObject.transform.childCount > 0)
        {
            //col.gameObject.GetComponent<EnemyInfo>().TakeDamage()
        }
    }
}
