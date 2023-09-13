
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
    public void MuteAudio(bool paused)
    {
        Debug.Log("Paused");
        if (AudioListener.volume == 0)
        {
            // unmute audio
            AudioListener.volume = 1;

            // update button appearance
            var colors = pause_btn.colors;
            colors.selectedColor = Color.white;
            pause_btn.colors = colors;
            Change2MuteImage();
        }
        else
        {
            // mute audio           
            AudioListener.volume = 0;

            // update button appearance       
            var colors = pause_btn.colors;
            colors.selectedColor = Color.red;
            pause_btn.colors = colors;
            Change2UnmuteImage();
        }
    }

    public void Change2UnmuteImage()
    {
        // change to unmute button icon
        pause_btn.image.sprite = play_img;
    }

    public void Change2MuteImage()
    {
        // change to mute button icon
        pause_btn.image.sprite = pause_img;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

