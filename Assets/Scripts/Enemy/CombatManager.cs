using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{    
    public EnemySpawner[] enemySpawners;
    private MainUI mainUI;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 0;
    public int totalEnemies = 0;
    public int points = 0;

    void Start()
    {
        waveNumber = 0; 
        mainUI = FindObjectOfType<MainUI>();
    }

    public float GetWaveInterval()
    {
        return waveInterval;
    }

    void Update()
    {
        if (totalEnemies <=0 || waveNumber == 0)
        {
            timer += Time.deltaTime;
            if (timer >= waveInterval)
            {
                timer = 0;
                waveNumber++;
                StartWave();
            }
        }
        mainUI.UpdateUI();
    }

    void StartWave()
    {
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            enemySpawner.StartSpawning();
        }
    }

    public void OnEnemyDeath()
    {
        totalEnemies--;
    }
}
