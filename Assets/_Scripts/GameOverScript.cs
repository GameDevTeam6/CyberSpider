using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Quick Death for testing purposes
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Implement death screen
            PlayerDied();
        }

    }

    public void PlayerDied()
    {
        // Implement death screen
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
