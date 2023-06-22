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
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}
