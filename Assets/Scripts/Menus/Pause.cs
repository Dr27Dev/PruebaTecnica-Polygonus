using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    public bool isPaused;

    private void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        PlayerController.Instance.OnPause += TriggerPause;
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isPaused = false;
        }
    }

    public void TriggerPause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            isPaused = false;
            AudioManager.Instance.PlayClip(AudioManager.Instance.Resume);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isPaused = true;
            AudioManager.Instance.PlayClip(AudioManager.Instance.Pause);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void MainMenu()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.UIButton);
        SceneManager.LoadScene(0);
    }
}
