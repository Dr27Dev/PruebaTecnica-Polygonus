using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab_A;
    [SerializeField] private GameObject enemyPrefab_B;
    [SerializeField] private Vector2[] bounds = new Vector2[2];
    
    private List<GameObject> enemyPool = new List<GameObject>();
    
    [SerializeField] private Vector2 spawnTimeRange = new Vector2(3,10);
    private float currentSpawnTime, spawnTimer;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject enemyA = Instantiate(enemyPrefab_A, transform);
            enemyA.SetActive(false); enemyPool.Add(enemyA);
            GameObject enemyB = Instantiate(enemyPrefab_B, transform);
            enemyB.SetActive(false); enemyPool.Add(enemyB);
        }
        currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
        spawnTimer = 0;
        SpawnHandler();
    }

    private void Update()
    {
        if (spawnTimer >= currentSpawnTime)
        {
            SpawnHandler();
            currentSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            spawnTimer = 0;
        }
        spawnTimer += Time.deltaTime;
    }

    private void SpawnHandler()
    {
        int enemyAmount = Random.Range(1, 5);
        MovementPattern movementPattern = (MovementPattern)Random.Range(0, 2);
        StartCoroutine(SpawnHorde(enemyAmount, movementPattern));
    }

    private Vector2 GetRandomSpawnPoint()
    {
        float x = Random.Range(bounds[0].x, bounds[1].x);
        float y = Random.Range(bounds[0].y, bounds[1].y);
        return new Vector2(x,y);
    }

    private IEnumerator SpawnHorde(int amount, MovementPattern movementPattern)
    {
        Vector3 spawnPosition = GetRandomSpawnPoint();
        if (movementPattern == MovementPattern.Circle) spawnPosition = new Vector3(0, 7);
        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy(enemyPool, enemyPrefab_A, movementPattern, spawnPosition);
            yield return new WaitForSeconds(0.4f);
        }
    }
    
    private void SpawnEnemy(List<GameObject> pool, GameObject prefab, MovementPattern pattern, Vector3 newPosition)
    {
        GameObject enemy = pool.Find(b => !b.activeSelf);
        if (enemy == null)
        {
            enemy = Instantiate(prefab);
            pool.Add(enemy);
        }
        enemy.SetActive(true);
        enemy.GetComponent<Enemy>().ResetValues(newPosition, pattern);
    }
}
