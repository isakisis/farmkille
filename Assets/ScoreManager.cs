using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int playerScore = 0;

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        
    }

    public void UpdateScore(int score) {
        playerScore += score;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + playerScore;
    }
}
