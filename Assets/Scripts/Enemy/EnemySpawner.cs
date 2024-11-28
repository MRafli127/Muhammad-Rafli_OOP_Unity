using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy; 

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;

    void Start()
    {
        spawnCount = defaultSpawnCount;
    }

    public void StartSpawning()
    {
        if (!isSpawning && spawnedEnemy.level <= combatManager.waveNumber)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemy());
        }
    }

    void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            if (spawnedEnemy != null)
            {
                Enemy enemy = Instantiate(spawnedEnemy);
                enemy.GetComponent<Enemy>().combatManager = combatManager;
                enemy.GetComponent<Enemy>().enemySpawner = this;
                combatManager.totalEnemies++;
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        StopSpawning();
    }

    public void OnEnemyDeath()
    {
        totalKill++;
        totalKillWave++;

        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            totalKillWave = 0;
            spawnCount = defaultSpawnCount + (spawnCountMultiplier * multiplierIncreaseCount);
            multiplierIncreaseCount++;
        }
        combatManager.points += spawnedEnemy.level;
    }
}
