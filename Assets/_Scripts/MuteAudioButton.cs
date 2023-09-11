using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteAudioButton : MonoBehaviour
{
    [SerializeField] Button mute_btn;

    // change between images, depending on button state
    public Sprite mute_img;
    public Sprite unmute_img;
    public void MuteAudio(bool muted)
    {
        Debug.Log("Muted");
        if (AudioListener.volume == 0)
        {
            // unmute audio
            AudioListener.volume = 1;

            // update button appearance
            var colors = mute_btn.colors;
            colors.selectedColor = Color.white;
            mute_btn.colors = colors;
            Change2MuteImage();
        }
        else
        {
            // mute audio           
            AudioListener.volume = 0;

            // update button appearance       
            var colors = mute_btn.colors;
            colors.selectedColor = Color.red;
            mute_btn.colors = colors;
            Change2UnmuteImage();
        }
    }

    public void Change2UnmuteImage()
    {
        // change to unmute button icon
        mute_btn.image.sprite = unmute_img;
    }

    public void Change2MuteImage()
    {
        // change to mute button icon
        mute_btn.image.sprite = mute_img;
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
