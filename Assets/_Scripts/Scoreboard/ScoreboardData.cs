using System.Collections.Generic;

[System.Serializable]
public class ScoreboardData {
    public string name;
    public int score;
    public ScoreboardData(string name, int score) {
        this.name = name;
        this.score = score;
    }
}

[System.Serializable]
public class ScoreBoardSaveData {
    public List<ScoreboardData> scoreboardDatas;
}