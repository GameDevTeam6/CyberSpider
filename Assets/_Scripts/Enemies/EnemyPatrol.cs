using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = 1f;

    private Rigidbody2D rb;

    private bool playerInRange = false;

    private void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Decide on direction
        Vector2 moveDirection = IsFacingRight() ? Vector2.right : Vector2.left;
        // Move in that direction
        rb.velocity = moveDirection * moveSpeed;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player unranged");
            playerInRange = false;
        }
    }

    private bool IsFacingRight()
    {
        // Epsilon represents a tiny floating point (almost zero)s
        return transform.localScale.x > Mathf.Epsilon;
    }

    public void PlayerInRange(bool val)
    {
        playerInRange = val;
    }
}
