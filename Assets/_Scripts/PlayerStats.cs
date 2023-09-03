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

    // initial player stats values
    private float playerHealth = 100;
    private float playerSpeed = 3.0f;
    private float playerScore = 0;

    // set initial number of seconds until game lost
    private float playerTime = 30;
    private bool isTimerRunning = true;

    private void Update()
    {
        // track current game stats
        healthText.text = playerHealth + "/100";
        speedText.text = "Speed: " + playerSpeed;
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
    }

    public float ChangeHealth(float health)
    {
        playerHealth += health;
        return playerHealth;
    }

    public void ChangeSpeed(float newSpeed)
    {
        playerSpeed += newSpeed;
        gameObject.GetComponent<PlayerController>()._speed = newSpeed;
    }

    public float ChangeScore(float newScore)
    {
        playerScore += newScore;
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
}
