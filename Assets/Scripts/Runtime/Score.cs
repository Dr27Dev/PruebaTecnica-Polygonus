using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;
    private int currentScore;
    private int highScore;
    
    private void Awake()
    {
        Instance = this;
        if (PlayerPrefs.HasKey("HighScore")) highScore = PlayerPrefs.GetInt("HighScore");
    }
    
    private void Update()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
    
    public void AddScore(int score) { currentScore += score; }
}
