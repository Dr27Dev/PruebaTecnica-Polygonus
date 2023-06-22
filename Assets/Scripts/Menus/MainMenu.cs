using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsMenu;

    private void Start()
    {
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        AudioManager.Instance.PlayClip(AudioManager.Instance.UIButton);
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}
