using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    private int currentScore;
    private int highScore;
    
    private void Awake()
    {
        Instance = this;
        if (PlayerPrefs.HasKey("HighScore")) highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = $"High Score: \n{highScore}";
    }
    
    private void Update()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = $"High Score: \n{highScore}";
        }
        scoreText.text = $"Score: \n{currentScore}";
    }
    
    public void AddScore(int score) { currentScore += score; }
}
