using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    public GameObject gameOverPanel;

    private int playerScore = 0;
    private float timeRemaining = 60;

    private bool gameOver = false;

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        if (!gameOver) {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();

            if (timeRemaining <= 0)
            {
                Time.timeScale = 0;

                gameOverPanel.SetActive(true);
                gameOver = true;
            }
        }
    }

    public void UpdateScore(int score) {
        playerScore += score;
        UpdateScoreText();
    }

    public void TryAgain()
    {
        Time.timeScale = 1;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + playerScore;
    }

    private void UpdateTimerText()
    {
        timerText.text = "Time: " + ((int) timeRemaining);
    }
}
