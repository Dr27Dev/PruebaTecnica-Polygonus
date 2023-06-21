using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private int pointsGiven = 5;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            Score.Instance.AddScore(pointsGiven);
            PickupSpawner.Instance.IsPickupActive = false;
            gameObject.SetActive(false);
        }
    }
}
