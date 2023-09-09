using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusShot : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    public float speed = 20f;
    public Rigidbody2D rb;

    private Vector3 shotDirection;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        shotDirection = (transform.position - playerPos.position).normalized;
        rb.velocity = shotDirection * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
