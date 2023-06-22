using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    public static EnemyBulletManager Instance;
    
    [SerializeField] private GameObject enemyBulletPrefab;
    private List<GameObject> enemyBulletPool = new List<GameObject>();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, transform);
            bullet.SetActive(false); enemyBulletPool.Add(bullet);
        }
    }
    
    public void EnemyBulletPool(Transform enemyTransform)
    {
        GameObject bullet = enemyBulletPool.Find(b => !b.activeSelf);
        if (bullet == null)
        {
            bullet = Instantiate(enemyBulletPrefab);
            enemyBulletPool.Add(bullet);
        }
        bullet.SetActive(true);
        bullet.transform.position = enemyTransform.position;
    }
}
