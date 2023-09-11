using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteAudioButton : MonoBehaviour
{
    [SerializeField] Button mute_btn;
    public void MuteAudio(bool muted)
    {
        Debug.Log("Muted");
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            var colors = mute_btn.colors;
            colors.selectedColor = Color.white;
            mute_btn.colors = colors;
        }
        else
        {
            AudioListener.volume = 0;
        }
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
