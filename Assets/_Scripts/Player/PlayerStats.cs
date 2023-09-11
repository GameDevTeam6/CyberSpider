using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    // initialize text fields associated with player stats
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timerText;

    [SerializeField] GameOverScript gameOverScript;
    private PlayerController playerController;

    // initial player stats values
    private float playerHealth = 100;
    private float playerSpeed = 3.0f;
    private readonly float standardSpeed = 3.0f;
    private float playerScore = 0;

    // set initial number of seconds until game lost
    private float playerTime = 120;
    private bool isTimerRunning = true;

    private float speedTimer = 0;
    private bool isSpeedTimerRunning = false;

    public static float finalScore;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        finalScore = 0;
    }

    private void Update()
    {
        // track current game stats
        healthText.text = playerHealth + "/100";
        scoreText.text = "" + playerScore;

        /////////////// TIMER CONTROLS ////////////////

        // format timer input, inspired by:
        // https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
        float mins = Mathf.FloorToInt(playerTime / 60);
        float secs = Mathf.FloorToInt(playerTime % 60);
        // set timer text
        timerText.text = string.Format("{0:00}:{1:00}", mins, secs);

        // if there is time remaining
        if (playerTime > 1 && isTimerRunning == true)
        {
            // proceed countdown
            playerTime -= Time.deltaTime;
        }
        else
        {
            // otherwise - game over
            isTimerRunning = false;
            SceneManager.LoadScene("ScoreboardMenu", LoadSceneMode.Single);
        }

        /////////////// Speed Timer ////////////////

        float speedMins = Mathf.FloorToInt(speedTimer / 60);
        float speedSecs = Mathf.FloorToInt(speedTimer % 60);
        // set timer text
        speedText.text = string.Format("{0:00}:{1:00}", speedMins, speedSecs);
        //speedText.text = "Speed: " + playerSpeed + "\n" + string.Format("{0:00}:{1:00}", speedMins, speedSecs);

        // if there is time remaining
        if (speedTimer > 1 && isSpeedTimerRunning == true)
        {
            // proceed countdown
            speedTimer -= Time.deltaTime;
        }
        else if (speedTimer < 1 && isSpeedTimerRunning == true)
        {
            // Revert to normal speed
            RevertSpeed();
            isSpeedTimerRunning = false;
        }

        /////////////// Game Over ////////////////
        ///
        if (playerHealth < 1)
        {
            PlayerDie();
        }
    }

    public void ProcessBuff(Item item)
    {
        if (item.actionType == ActionType.Health)
        {
            ChangeHealth(item.actionValue);
        } else if (item.actionType == ActionType.Speed)
        {
            ChangeSpeed(item.actionValue);
        } else if (item.actionType == ActionType.Score)
        {
            ChangeScore(item.actionValue);
        }
    }

    private void PlayerDie()
    {
        playerController.PlayerDie();
        gameOverScript.PlayerDied();
        Time.timeScale = 0;
        finalScore = playerScore;
    }

    public float ChangeHealth(float health)
    {
        playerHealth += health;
        return playerHealth;
    }

    public void ChangeSpeed(float newSpeed)
    {
        playerSpeed = newSpeed;
        speedTimer += 10;
        gameObject.GetComponent<PlayerController>()._speed = newSpeed;
        isSpeedTimerRunning = true;
    }

    public void RevertSpeed()
    {
        playerSpeed = 3f;
        gameObject.GetComponent<PlayerController>()._speed = standardSpeed;
    }

    public float ChangeScore(float newScore)
    {
        playerScore += newScore;
        PlayerStats.finalScore += newScore;
        return playerScore;
    }

    public float GetSpeed()
    {
        return playerSpeed;
    }

    public float GetHealth()
    {
        return playerHealth;
    }

    public float GetScore()
    {
        return playerScore;
    }

    public float GetTime()
    {
        return playerTime;
    }

}
