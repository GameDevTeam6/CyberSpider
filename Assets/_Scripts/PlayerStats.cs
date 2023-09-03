using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text scoreText;
    private float playerHealth = 100;
    private float playerSpeed = 3.0f;
    private float playerScore = 0;

    private void Update()
    {
        // track current game stats
        healthText.text = playerHealth + "/100";
        speedText.text = "Speed: " + playerSpeed;
        scoreText.text = "" + playerScore;
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
