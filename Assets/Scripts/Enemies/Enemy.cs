using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyGun))]
public class Enemy : MonoBehaviour
{
    private EnemyMovement enemyMovement;

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void Deactivate()
    {
        
        gameObject.SetActive(false);
    }

    public void ResetValues(Vector3 newPosition, MovementPattern pattern)
    {
        enemyMovement.ResetValues(newPosition, pattern);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DeactivationZone")) Deactivate();
    }
}
