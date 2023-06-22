using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] private float fireRate = 2.5f;
    private float fireTimer;
    
    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= 1f / fireRate)
        {
            EnemyBulletManager.Instance.EnemyBulletPool(transform);
            fireTimer = 0;
        }
    }

}
