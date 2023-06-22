using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupSpawner : MonoBehaviour
{
    public static PickupSpawner Instance;
    
    [SerializeField] private GameObject pickupPrefab;
    [SerializeField] private Vector2[] bounds = new Vector2[2];
    
    private List<GameObject> pickupPool = new List<GameObject>();
    
    [SerializeField] private Vector2 spawnTimeRange = new Vector2(3,6);
    private float currentSpawnTime, spawnTimer;
    
    public int ActivePickups;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject pickup = Instantiate(pickupPrefab, transform);
            pickup.SetActive(false); pickupPool.Add(pickup);
        }
        currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
        spawnTimer = 0;
        SpawnPickup(pickupPool, pickupPrefab);
    }

    private void Update()
    {
        if (ActivePickups <= 5)
        {
            if (spawnTimer >= currentSpawnTime)
            {
                SpawnPickup(pickupPool, pickupPrefab);
                currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
                spawnTimer = 0;
                ActivePickups++;
            }
            spawnTimer += Time.deltaTime;
        }
    }

    private Vector2 GetRandomSpawnPoint()
    {
        float x = Random.Range(bounds[0].x, bounds[1].x);
        float y = Random.Range(bounds[0].y, bounds[1].y);
        return new Vector2(x,y);
    }
    
    private void SpawnPickup(List<GameObject> pool, GameObject prefab)
    {
        GameObject pickup = pool.Find(b => !b.activeSelf);
        if (pickup == null)
        {
            pickup = Instantiate(prefab);
            pool.Add(pickup);
        }
        pickup.SetActive(true);
        pickup.transform.position = GetRandomSpawnPoint();
    }
}
