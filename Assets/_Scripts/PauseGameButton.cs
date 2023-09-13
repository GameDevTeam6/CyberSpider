
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameButton : MonoBehaviour
{
    [SerializeField] Button pause_btn;

    // change between images, depending on button state
    public Sprite pause_img;
    public Sprite play_img;

    private bool is_paused;

    void Start()
    {
        is_paused = false;
    }

    public void PauseButton(bool paused)
    {      
        if (is_paused)
        {
            Debug.Log("Game Paused");
            // unmute audio
            ResumeGame();
            is_paused = false;
            AudioListener.pause = false;
        }
        else
        {
            Debug.Log("Game Resumed");
            // pause game;           
            PauseGame();
            is_paused = true;
            AudioListener.pause = true;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

}