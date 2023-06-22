using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource audioSource;

    public AudioClip EnemyExplosion;
    public AudioClip UIButton;
    public AudioClip GameOver;
    public AudioClip Pause;
    public AudioClip Resume;
    public AudioClip Pickup;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
    
}
