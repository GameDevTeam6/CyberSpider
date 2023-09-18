using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class LevelPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCol;
    [SerializeField] Color openCol;
    [SerializeField] Color closedCol;

    public AudioClip puzzleSolvedSound; // This is the PuzzleSolved sound effect
    private AudioSource audioSource; // AudioSource to play the sound effect

    public bool locked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        audioSource = gameObject.AddComponent<AudioSource>();
        if (locked == true)
        {
            LockPlatform();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.transform.position.y > transform.position.y)
        {
            Debug.Log("Player exited above");
            LockPlatform();
        }
    }

    private void LockPlatform()
    {
        boxCol.isTrigger = false;
        GetComponent<SpriteRenderer>().color = closedCol;
        audioSource.PlayOneShot(puzzleSolvedSound);
        locked = true;
    }
}
