using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] float moveDistance;
    [SerializeField] float moveSpeed;
    private Vector3 initialPos;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < initialPos.x + moveDistance)
        {
            //rb.AddForce(new Vector3(moveSpeed, 0f, 0f));
            transform.Translate(new Vector3(moveSpeed, 0f, 0f));
        } else if (transform.position.x < initialPos.x - moveDistance)
        {
            transform.Translate(new Vector3(-moveSpeed, 0f, 0f));
            //rb.AddForce(new Vector3(-moveSpeed, 0f, 0f));
        }
    }

    private void MoveLeft()
    {

    }
}
