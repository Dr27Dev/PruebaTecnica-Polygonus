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
    [SerializeField] private GameObject deathFXprefab;

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void Deactivate()
    {
        var deathFX = Instantiate(deathFXprefab);
        deathFX.transform.position = transform.position;
        Destroy(deathFX, 1f);
        AudioManager.Instance.PlayClip(AudioManager.Instance.EnemyExplosion);
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
