using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private Vector2 fireRateRange = new Vector2(1, 5);
    [SerializeField] private float fireRate = 2.5f;
    private float fireTimer;
    
    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= 1f / fireRate)
        {
            EnemyBulletManager.Instance.EnemyBulletPool(bulletSpawnPosition);
            fireRate = Random.Range(fireRateRange.x, fireRateRange.y);
            fireTimer = 0;
        }
    }

}
