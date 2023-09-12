using TMPro;
using UnityEngine;

public class ShowScoreboard : MonoBehaviour {

    private Transform scoreShowBarTemplate;

    private void Awake() {
        // Get Template of ScoreBar
        scoreShowBarTemplate = transform.Find("scoreShowBarTemplate");
    }

    private void Start() {
        // Get And Check All ScoreBoard Data Count
        int indexCount = ScoreboardManager.Instance.GetAllScoreBoardData().Count;
        if(indexCount < 0) return;

        // Sorting Algoritm
        for (int i = 0; i < ScoreboardManager.Instance.GetAllScoreBoardData().Count; i++) {
            for (int j = 0; j < ScoreboardManager.Instance.GetAllScoreBoardData().Count; j++) {
                if(ScoreboardManager.Instance.GetAllScoreBoardData()[j].score < ScoreboardManager.Instance.GetAllScoreBoardData()[i].score) {
                   ScoreboardData higherData = ScoreboardManager.Instance.GetAllScoreBoardData()[i];
                   ScoreboardManager.Instance.GetAllScoreBoardData()[i] = ScoreboardManager.Instance.GetAllScoreBoardData()[j];
                   ScoreboardManager.Instance.GetAllScoreBoardData()[j] = higherData; 
                }
            }
        }

        // Make a Copy of ScoreBar According to the Data
        for (int i = 0; i < ScoreboardManager.Instance.GetAllScoreBoardData().Count; i++) {
            if(i > 9) return;
            Transform cloneBar = Instantiate(scoreShowBarTemplate);
            cloneBar.parent = transform;
            cloneBar.localScale = new Vector3(1, 1, 1);
            ScoreboardData data = ScoreboardManager.Instance.GetAllScoreBoardData()[i];
            cloneBar.GetComponentInChildren<TMP_Text>().text = $"({i + 1})   {data.name}   {data.score}";
            cloneBar.gameObject.SetActive(true);
        }
    }
}