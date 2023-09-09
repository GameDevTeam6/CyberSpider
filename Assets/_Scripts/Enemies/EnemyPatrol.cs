using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private float moveSpeed = 1f;
    //private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            //rb.velocity = new Vector2(moveSpeed, 0f);
            transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0f);
        } else
        {
            transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        //transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), transform.localScale.y);
    }

    private bool IsFacingRight()
    {
        // Epsilon represents a tiny floating point (almost zero)s
        return transform.localScale.x > Mathf.Epsilon;
    }
}
