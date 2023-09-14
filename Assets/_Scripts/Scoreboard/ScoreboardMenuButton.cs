using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


public class ScoreboardMenuButton : MonoBehaviour
{
    public Image transitionPanel;
    public float transitionSpeed = 0.5f;

    public AudioClip buttonPressedClip;
    private AudioSource audioSource;
    public AudioMixer audioMixer;

    public Slider loadingBar;

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

    public void StartScoreboard()
    {
        // Play the button pressed sound
        if (buttonPressedClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonPressedClip);
        }

        // Start the scene transition
        StartCoroutine(TransitionToScene("ScoreboardMenu"));
    }

    IEnumerator TransitionToScene(string sceneName)
    {
        // Fade in (from transparent to opaque)
        while (transitionPanel.color.a < 1.0f)
        {
            transitionPanel.color = new Color(transitionPanel.color.r, transitionPanel.color.g, transitionPanel.color.b, transitionPanel.color.a + (Time.deltaTime * transitionSpeed));
            yield return null;
        }

        // Show loading bar and reset its value
        loadingBar.gameObject.SetActive(true);
        loadingBar.value = 0;

        // Asynchronously load the desired scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // Prevent scene from activating immediately

        while (!asyncLoad.isDone)
        {
            // Update the loading bar based on the loading progress
            loadingBar.value = asyncLoad.progress;

            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true; // Activate the scene when loading is almost complete
            }

            yield return null;
        }

        // Hide loading bar
        loadingBar.gameObject.SetActive(false);

        // Fade out (from opaque to transparent) in the new scene
        while (transitionPanel.color.a > 0)
        {
            transitionPanel.color = new Color(transitionPanel.color.r, transitionPanel.color.g, transitionPanel.color.b, transitionPanel.color.a - (Time.deltaTime * transitionSpeed));
            yield return null;
        }
    }
}