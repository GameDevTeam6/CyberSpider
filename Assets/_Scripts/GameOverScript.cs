using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] InputManager editor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("ScoreboardMenu", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Debug.Log("Toggling Puzzle...");
            editor.OpenInputField(() =>
            {

            });
        }
    }
}
