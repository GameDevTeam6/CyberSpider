using System.Collections.Generic;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour {

    // Create Instance
    public static ScoreboardManager Instance { get; private set; }

    // All Scoreboard Data List
    [SerializeField] private List<ScoreboardData> scoreboardData = new List<ScoreboardData>();

    // Current Score At that Level
    private int currentScore;
    // Current Name At thet Level
    private string currentName;


    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else {
            Destroy(gameObject);
        }
    }

    private void Start() { 
        LoadScoreBoard();
    }

    // Add To ScoreBoard
    public void AddToScoreboard(ScoreboardData data) {
        scoreboardData.Add(data);
    }

    // Set the Current Score
    public void SetCurrentScore(int _score) {
        currentScore = _score;
    }

    // Get the Current Score
    public int GetCurrentScore() {
        return currentScore;
    }

    // Set the Current Name
    public void SetCurrentName(string _name) {
        currentName = _name;
    }

    // Getting the Current Name 
    public string GetCurrentName() {
        return currentName;
    }

    // Get All Board Data
    public List<ScoreboardData> GetAllScoreBoardData() {
        return scoreboardData;
    }

    // Set Score Board Data
    public void SetScoreBoardData(List<ScoreboardData> scoreboardDataList) {
        scoreboardData = scoreboardDataList;
    }

    // Save All Data
    public void SaveScoreBoard() {
        ScoreBoardSaveData savedata = new ScoreBoardSaveData {
            scoreboardDatas = GetAllScoreBoardData()
        };

        string json = JsonUtility.ToJson(savedata);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
    }

    // Load All Data
    public void LoadScoreBoard() {
        string getSaveData = PlayerPrefs.GetString("highscoreTable", "{}");
        ScoreBoardSaveData savedata = JsonUtility.FromJson<ScoreBoardSaveData>(getSaveData);
        SetScoreBoardData(savedata.scoreboardDatas);
    }
}