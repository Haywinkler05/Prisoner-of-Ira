using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class EnemySpawnEntry
{
    public GameObject prefab;
    public int weight = 1;
}

[System.Serializable]
public class WaveSpawnConfig
{
    public EnemySpawnEntry[] easy;
    public EnemySpawnEntry[] medium;
    public EnemySpawnEntry[] hard;
}

public class spawnEnemy : MonoBehaviour
{
    [Header("Spawning Vars")]
    [SerializeField] private Transform spawnArea;
    [SerializeField] private float spawnRadius = 1.0f;

    [Header("Wave Configs")]
    [SerializeField] private WaveSpawnConfig[] waveConfigs;
    private bool hardEnemySpawnedThisWave = false;
    public void spawn()
    {
        GameObject prefab = GetEnemyForCurrentWave();
        if (prefab == null) return;

        Vector2 spawnPos = (Vector2)spawnArea.position + Random.insideUnitCircle * spawnRadius;
        Instantiate(prefab, spawnPos, Quaternion.identity);
        gameManager.instance.enemiesAlive++;
        gameManager.instance.enemiesSpawnedThisWave++;
    }

    private GameObject GetEnemyForCurrentWave()
    {
        int waveIndex = Mathf.Clamp(gameManager.instance.currentWave - 1, 0, waveConfigs.Length - 1);
        WaveSpawnConfig config = waveConfigs[waveIndex];

        // Check if we should exclude hard enemies
        bool allowHard = gameManager.instance.currentWave >= 3 && !hardEnemySpawnedThisWave;

        List<GameObject> pool = new List<GameObject>();

        foreach (var entry in config.easy)
            for (int i = 0; i < entry.weight; i++)
                pool.Add(entry.prefab);

        foreach (var entry in config.medium)
            for (int i = 0; i < entry.weight; i++)
                pool.Add(entry.prefab);

        if (allowHard)
        {
            foreach (var entry in config.hard)
                for (int i = 0; i < entry.weight; i++)
                    pool.Add(entry.prefab);
        }

        if (pool.Count == 0) return null;

        GameObject chosen = pool[Random.Range(0, pool.Count)];

        // Check if chosen is a hard enemy
        if (chosen != null && config.hard.Length > 0)
        {
            foreach (var entry in config.hard)
            {
                if (entry.prefab == chosen)
                {
                    hardEnemySpawnedThisWave = true;
                    break;
                }
            }
        }

        return chosen;
    }
    public void ResetWave()
    {
        hardEnemySpawnedThisWave = false;
    }
}