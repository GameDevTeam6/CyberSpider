using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;
    [HideInInspector] public Vector3 movement;

    private enum direction
    {
        left,
        right
    };
    [SerializeField] private direction initialDirection;

    private Vector3 initialPos;
    private Rigidbody2D rb;

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
        if (initialDirection == direction.left)
        {
            if (transform.position.x > initialPos.x - moveDistance)
            {
                initialDirection = direction.left;
            }
            else if (transform.position.x < initialPos.x - moveDistance)
            {
                initialDirection = direction.right;
            }
        }

        if (initialDirection == direction.right)
        {
            if (transform.position.x < initialPos.x + moveDistance)
            {
                initialDirection = direction.right;
            }
            else if (transform.position.x > initialPos.x + moveDistance)
            {
                initialDirection = direction.left;
            }
        }
    }

    private void MovePlat()
    {
        if (initialDirection == direction.left)
        {
            movement = new Vector3(1 * -moveSpeed, 0f, 0f);
        }
        else
        {
            movement = new Vector3(1 * moveSpeed, 0f, 0f);
        }

        transform.Translate(movement);
    }
}
