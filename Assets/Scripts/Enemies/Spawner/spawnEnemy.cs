using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    [Header("Spawning Vars")]
    [SerializeField] private Transform spawnArea;
    [SerializeField] private float spawnRadius = 1.0f;
    [SerializeField] private int maxSpawnAtOnce = 2;
    [Header("Spawning")]
    public GameObject[] enemyTypes;


    public void spawn()
    {
        int randomNum = Random.Range(0, enemyTypes.Length);
        Vector2 spawnPos = (Vector2)spawnArea.position + Random.insideUnitCircle * spawnRadius;
        Instantiate(enemyTypes[randomNum], spawnPos, Quaternion.identity);
        gameManager.instance.enemiesAlive++;
        gameManager.instance.enemiesSpawnedThisWave++;
      }
   
}
