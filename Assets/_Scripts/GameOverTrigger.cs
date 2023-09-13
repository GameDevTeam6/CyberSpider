using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour {
    public event EventHandler OnPlayerEndGame; // GameOver Event
    [SerializeField] private PlayerStats playerStats; // Get Player Stats to Store Player Score

    private void OnTriggerEnter2D(Collider2D other) {
        // Compare Player Tag
        if(other.CompareTag("Player")) {
            // Trigger Event the Afert Entering The Game End Level
            if(OnPlayerEndGame != null) {  OnPlayerEndGame(this, EventArgs.Empty); } 
        }
    }

    // Subscribe GameOver Event
    private void Awake() => OnPlayerEndGame += GameEnded;

    // What Happen after Trigger the Game End Event
    private void GameEnded(object sender, EventArgs e) {
        // Store the Player Score
        ScoreboardManager.Instance.SetCurrentScore((int)playerStats.GetScore());
        // Load GameOver Scene
        SceneManager.LoadScene(TagManager.SCENE_GAMEOVER_NAME);
    }
}
