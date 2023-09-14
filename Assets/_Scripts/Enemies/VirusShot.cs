using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VirusShot : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [HideInInspector] public int damage;
    [HideInInspector] public Vector3 shotDirection;

    public float speed = 2f;
    public Rigidbody2D rb;

    public AudioClip virusHitSound; // This is the VirusHit sound effect
    private AudioSource audioSource; // AudioSource to play the sound effect

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = shotDirection * speed;

        // Initialize the audio source
        audioSource = gameObject.AddComponent<AudioSource>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Hit player");
            player.GetComponent<PlayerStats>().ChangeHealth(-damage);

            // Play the VirusHit sound effect
            if (virusHitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(virusHitSound);
            }

            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}
