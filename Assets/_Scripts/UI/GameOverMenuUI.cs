using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenuUI : MonoBehaviour {

    // To Get the Submit Button
    [SerializeField] private Button submitBtn;
    // To Get the MainMenu Button
    [SerializeField] private Button mainmenuBtn;

    [SerializeField] private TMP_InputField nameSetter;

    // Random Names
    private string[] randomNames = new string[] {"Jac", "Simon","Karl","David","Kyran",
    "Ray","Steve","Ben","Osian","Jimmy","Maya"};

    private void Awake() {
        // Init Buttons

        // When Submit Button Clicks
        submitBtn.onClick.AddListener(delegate () {

            // Apply Random Name OR Given Name
            if(nameSetter.text == "") {
                string characterName = randomNames[Random.Range(0, randomNames.Length)];
                ScoreboardManager.Instance.SetCurrentName(characterName);
            } else {
                ScoreboardManager.Instance.SetCurrentName(nameSetter.text);
            }

            // Get Current Name and Current Score
            string currentName = ScoreboardManager.Instance.GetCurrentName();
            int currentScore = ScoreboardManager.Instance.GetCurrentScore();

            // Add To New Score To Board
            ScoreboardManager.Instance.AddToScoreboard(new ScoreboardData(currentName, currentScore));

            // Save When Submit
            ScoreboardManager.Instance.SaveScoreBoard();

            SceneManager.LoadScene(TagManager.SCENE_MAINMENU_NAME);
        });

        // When Mainmenu Button Clicks
        mainmenuBtn.onClick.AddListener(delegate () {
            SceneManager.LoadScene(TagManager.SCENE_MAINMENU_NAME);
        });
    }
}