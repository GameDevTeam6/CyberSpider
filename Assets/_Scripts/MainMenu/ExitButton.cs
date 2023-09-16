using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ExitButton : MonoBehaviour
{
    public Image transitionPanel;
    public float transitionSpeed = 0.5f;

    public AudioClip buttonPressedClip;
    private AudioSource audioSource;
    public AudioMixer audioMixer;

    void Start()
    {
        // Start with the panel fully transparent
        if (transitionPanel != null)
        {
            transitionPanel.color = new Color(transitionPanel.color.r, transitionPanel.color.g, transitionPanel.color.b, 0);
        }

        // Initialize the audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
    }

    public void ExitGame()
    {
        // Play the button pressed sound
        if (buttonPressedClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonPressedClip);
        }

        // Start the exit transition
        StartCoroutine(ExitTransition());
    }

    IEnumerator ExitTransition()
    {
        // Fade in (from transparent to opaque)
        while (transitionPanel.color.a < 1.0f)
        {
            transitionPanel.color = new Color(transitionPanel.color.r, transitionPanel.color.g, transitionPanel.color.b, transitionPanel.color.a + (Time.deltaTime * transitionSpeed));
            yield return null;
        }

        // Exit the game
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
