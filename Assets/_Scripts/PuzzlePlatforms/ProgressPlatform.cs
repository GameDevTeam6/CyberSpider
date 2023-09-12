using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ProgressPlatform : MonoBehaviour
{
    [SerializeField] Color lockedColor;
    [SerializeField] Color unlockedColor;

    public bool solved = false;

    private Rigidbody2D rb;

    public AudioClip puzzleSolvedSound; // This is the PuzzleSolved sound effect
    private AudioSource audioSource; // AudioSource to play the sound effect


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;

        // Initialize the audio source
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (solved)
        {
            UnlockPlatform();
            rb.simulated = true;
        }
        
    }

    private void UnlockPlatform()
    {
        GetComponent<SpriteRenderer>().color = unlockedColor;
        // Play the PuzzleSolved sound effect
        if (puzzleSolvedSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(puzzleSolvedSound);
        }
    }
}
