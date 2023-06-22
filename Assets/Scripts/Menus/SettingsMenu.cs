using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider Master;
    public Slider Music;
    public Slider SFX;
    [SerializeField] private Toggle fullscreenToggle;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Volume_Master")) Master.value = PlayerPrefs.GetFloat("Volume_Master");
        else Master.value = 1;
        if(PlayerPrefs.HasKey("Volume_BGM")) Music.value = PlayerPrefs.GetFloat("Volume_BGM");
        else Music.value = 1;
        if(PlayerPrefs.HasKey("Volume_SFX")) SFX.value = PlayerPrefs.GetFloat("Volume_SFX");
        else SFX.value = 1;
        
        Master.onValueChanged.AddListener(SetLevel);
        Music.onValueChanged.AddListener(SetLevelMusic);
        SFX.onValueChanged.AddListener(SetLevelSFX);

        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            var fullscreenValue = PlayerPrefs.GetInt("Fullscreen", 1); // 0 is false, 1 is true
            if (fullscreenValue == 1) {fullscreenToggle.isOn = true; Fullscreen(true);}
            else {fullscreenToggle.isOn = false; Fullscreen(false);}
        }
        else
        {
            Fullscreen(true);
        }
    }

    private void Update()
    {
        float aspect = 1.333333f;
        print(Camera.main.aspect);
        if (Camera.main.aspect < (aspect-0.1f) || Camera.main.aspect > (aspect+0.1f))
            Screen.SetResolution(1024,768, Screen.fullScreen);
    }

    private void Start()
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(Master.value) * 20);
        mixer.SetFloat("MusicVol", Mathf.Log10(Music.value) * 20);
        mixer.SetFloat("SFXVol", Mathf.Log10(SFX.value) * 20);
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    public void Fullscreen(bool value)
    {
        Screen.fullScreen = value;
        if (value) PlayerPrefs.SetInt("Fullscreen", 1);
        else PlayerPrefs.SetInt("Fullscreen", 0);
    }
    
    public void SetLevel(float SliderValue)
    {
        float value = Mathf.Log10(SliderValue) * 20;
        mixer.SetFloat("MasterVol", value);
        PlayerPrefs.SetFloat("Volume_Master", SliderValue);
    }

    public void SetLevelMusic(float SliderValue)
    {
        float value = Mathf.Log10(SliderValue) * 20;
        mixer.SetFloat("MusicVol", value);
        PlayerPrefs.SetFloat("Volume_BGM", SliderValue);
    }
    public void SetLevelSFX(float SliderValue)
    {
        float value = Mathf.Log10(SliderValue) * 20;
        mixer.SetFloat("SFXVol", value);
        PlayerPrefs.SetFloat("Volume_SFX", SliderValue);
    }


}
