using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text speedText;
    private float playerHealth = 100;
    private int playerLives = 3;
    private float playerSpeed = 3.0f;

    private void Update()
    {
        healthText.text = "HP: " + playerHealth + "/100";
        speedText.text = "Player speed: " + playerSpeed;

    }

    public void ChangeHealth(float health)
    {
        playerHealth += health;
    }

    public void ChangeSpeed(float newSpeed)
    {
        playerSpeed += newSpeed;
        gameObject.GetComponent<PlayerController>()._speed = newSpeed;
    }

    public float GetSpeed()
    {
        return playerSpeed;
    }

    public void ProcessBuff(Item item)
    {
        if (item.actionType == ActionType.Health)
        {
            ChangeHealth(item.actionValue);
        } else if (item.actionType == ActionType.Speed)
        {
            ChangeSpeed(item.actionValue);
        }
    }
}
