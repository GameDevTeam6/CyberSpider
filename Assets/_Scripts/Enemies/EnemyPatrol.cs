using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private float moveSpeed = 1f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (IsFacingRight())
        //{
        //    rb.velocity = new Vector2(moveSpeed, 0f);
        //} else
        //{
        //    rb.velocity = new Vector2(-moveSpeed, 0f);
        //}

        // Decide on direction
        Vector2 moveDirection = IsFacingRight() ? Vector2.right : Vector2.left;
        // Move in that direction
        rb.velocity = moveDirection * moveSpeed;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    private bool IsFacingRight()
    {
        // Epsilon represents a tiny floating point (almost zero)s
        return transform.localScale.x > Mathf.Epsilon;
    }
}
