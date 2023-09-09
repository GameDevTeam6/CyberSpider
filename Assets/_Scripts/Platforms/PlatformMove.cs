using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] float moveDistance;
    [SerializeField] float moveSpeed;
    private Vector3 initialPos;
    private Rigidbody2D rb;

    private enum direction
    {
        left,
        right
    };

    direction dir = direction.left;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPos = transform.position;
    }

    void FixedUpdate()
    {
        CheckDir();
        MovePlat();
    }

    private void CheckDir()
    {
        if (dir == direction.left)
        {
            if (transform.position.x < initialPos.x + moveDistance)
            {
                dir = direction.left;
            }
            else if (transform.position.x > initialPos.x + moveDistance)
            {
                dir = direction.right;
            }
        }

        if (dir == direction.right)
        {
            if (transform.position.x > initialPos.x - moveDistance)
            {
                dir = direction.right;
            }
            else if (transform.position.x < initialPos.x - moveDistance)
            {
                dir = direction.left;
            }
        }
    }

    private void MovePlat()
    {
        if (dir == direction.left)
        {
            transform.Translate(new Vector3(1 * moveSpeed, 0f, 0f));
        }
        else
        {
            transform.Translate(new Vector3(1 * -moveSpeed, 0f, 0f));
        }
    }
}
