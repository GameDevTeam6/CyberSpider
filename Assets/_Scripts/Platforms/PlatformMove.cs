using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;
    [HideInInspector] public Vector3 movement;

    // Enum to determine initial direction
    private enum direction
    {
        left,
        right
    };
    [SerializeField] private direction initialDirection;

    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    void FixedUpdate()
    {
        // Check which direction to go
        CheckDir();
        // Move in that direction
        transform.Translate(movement);
    }

    /* 
     * Function that evaluations initial direction setting,
     * checks current position, and decides which way to do
    */
    private void CheckDir()
    {
        if (initialDirection == direction.left)
        {
            movement = new Vector3(1 * -moveSpeed, 0f, 0f);
            if (transform.position.x < initialPos.x - moveDistance)
            {
                initialDirection = direction.right;
            }
        } else if (initialDirection == direction.right)
        {
            movement = new Vector3(1 * moveSpeed, 0f, 0f);
            if (transform.position.x > initialPos.x + moveDistance)
            {
                initialDirection = direction.left;
            }
        }
    }
}
