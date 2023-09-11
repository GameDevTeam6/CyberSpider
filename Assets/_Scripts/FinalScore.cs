using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    [SerializeField] TMP_Text FinalScoreText;
    private float score = PlayerStats.finalScore;

    // Start is called before the first frame update
    void Start()
    {
        FinalScoreText.text = score + "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
