using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private bool isGameOver;

    private void Start()
    {
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);
        isGameOver = false;
        PlayerController.Instance.OnReset += RestartLevel;
    }

    private void SetGameOver()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.GameOver);
        Time.timeScale = 0;
        isGameOver = true;
        gameOverScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartLevel()
    {
        if (isGameOver)
        {
            Time.timeScale = 1;
            AudioManager.Instance.PlayClip(AudioManager.Instance.UIButton);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            isGameOver = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Enemy A") || col.transform.CompareTag("Enemy B") || col.transform.CompareTag("EnemyBullet"))
        {
            SetGameOver();
        }
    }
}
