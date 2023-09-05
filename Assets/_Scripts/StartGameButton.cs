using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class StartGameButton : MonoBehaviour
{
    public Image transitionPanel; // Drag the Transition Panel Image component here in the inspector
    public float transitionSpeed = 0.5f; // Adjust this value for desired transition speed

    public AudioClip buttonPressedClip; // Drag your ButtonPressed.wav here in the inspector
    private AudioSource audioSource;
    public AudioMixer audioMixer;

    public Slider loadingBar; // Drag the LoadingBar slider component here

    // Start is called before the first frame update
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

    public void StartGame(string LevelName)
    {
        // Play the button pressed sound
        if (buttonPressedClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonPressedClip);
        }

        // Start the scene transition
        StartCoroutine(TransitionToScene("Gameplay"));
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Gameplay");
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