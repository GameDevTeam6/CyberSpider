using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ProgressPlatform : MonoBehaviour
{
    [SerializeField] Color lockedColor;
    [SerializeField] Color unlockedColor;

    public bool solved = false;
    public bool checkPlatState = true;

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
        if (solved && checkPlatState)
        {
            UnlockPlatform();
            rb.simulated = true;
            checkPlatState = false;
        }
        
    }

    private void UnlockPlatform()
    {
        GetComponent<SpriteRenderer>().color = unlockedColor;
        // Play the PuzzleSolved sound effect
        if (puzzleSolvedSound != null && audioSource != null)
        {
            Debug.Log("Playing audio");
            audioSource.PlayOneShot(puzzleSolvedSound);
        }
    }
}
