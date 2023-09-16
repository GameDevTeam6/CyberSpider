using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PuzzleSolver : MonoBehaviour
{
    [SerializeField] InputManager inputManager;

    public AudioClip puzzleAlertSound; // This is the PuzzleAlert sound effect
    private AudioSource audioSource; // AudioSource to play the sound effect

    [SerializeField] CursorSettings cursorSettings;

    private PlayerInput playerInput;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize the audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        playerInput = gameObject.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("ProgressPuzzle"))
        {
            Debug.Log("Enter puzzle.");

            // Play the PuzzleAlert sound effect
            if (puzzleAlertSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(puzzleAlertSound);
            }
            playerInput.DeactivateInput();
            inputManager.OpenQuestionPanel(() => {
                //trig.gameObject.GetComponent<ProgressPuzzleInfo>().platform.transform.GetComponent<ProgressPlatform>().solved = true;
                trig.gameObject.GetComponent<ProgressPuzzleInfo>().UnlockPlatform();
                trig.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                trig.gameObject.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 150);

                // Call method to increase player points
                gameObject.GetComponent<PlayerStats>().PuzzleSolved();
                playerInput.ActivateInput();
            });
        }
    }
}
