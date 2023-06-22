using System;
using System.Collections;
using System.Collections.Generic;
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

    public void ResetValues(Vector3 newPosition)
    {
        enemyMovement.ResetValues(newPosition);
    }
}
