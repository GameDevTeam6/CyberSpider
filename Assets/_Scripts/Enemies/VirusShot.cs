using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusShot : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [HideInInspector] public int damage;
    [HideInInspector] public Vector3 shotDirection;

    public float speed = 2f;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = shotDirection * speed;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            player.GetComponent<PlayerStats>().ChangeHealth(-damage);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}
