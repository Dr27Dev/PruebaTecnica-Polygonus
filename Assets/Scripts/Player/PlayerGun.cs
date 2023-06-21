using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerGun : MonoBehaviour
{
    private PlayerController playerController;
    
    [SerializeField] private Transform poolingParent;
    [SerializeField] private GameObject bulletPrefab_A;
    [SerializeField] private GameObject bulletPrefab_B;
    private List<GameObject> bulletPool_A = new List<GameObject>();
    private List<GameObject> bulletPool_B = new List<GameObject>();

    [SerializeField] private float fireRate = 2.5f;
    private float fireTimer;
    
    private bool fire_A, fire_B;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerController.OnFire_A += TriggerFire_A;
        playerController.OnFire_B += TriggerFire_B;
    }
    
    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject bullet_A = Instantiate(bulletPrefab_A, poolingParent);
            bullet_A.SetActive(false); bulletPool_A.Add(bullet_A);
            GameObject bullet_B = Instantiate(bulletPrefab_B, poolingParent);
            bullet_B.SetActive(false); bulletPool_B.Add(bullet_B);
        }
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= 1f / fireRate)
        {
            if (fire_A) SpawnBullet(bulletPool_A, bulletPrefab_A);
            if (fire_B) SpawnBullet(bulletPool_B, bulletPrefab_B);
        }
    }

    private void TriggerFire_A(bool fire) { fire_A = fire; }
    private void TriggerFire_B(bool fire) { fire_B = fire; }

    private void SpawnBullet(List<GameObject> bulletPool, GameObject bulletPrefab)
    {
        fireTimer = 0f;
        GameObject bullet = bulletPool.Find(b => !b.activeSelf);
        if (bullet == null)
        {
            bullet = Instantiate(bulletPrefab);
            bulletPool.Add(bullet);
        }
        bullet.SetActive(true);
        bullet.transform.position = transform.position;
    }
}
