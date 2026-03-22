using UnityEngine;
public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    [Header("Wave Settings")]
    public int currentWave = 0;
    public int totalWaves = 3;
    public int enemiesAlive = 0;
    public int enemiesSpawnedThisWave = 0;
    public int maxEnemiesThisWave = 10;
    [Header("Active Enemy Limits Per Wave")]
    public int wave1ActiveLimit = 2;
    public int wave2ActiveLimit = 4;
    public int wave3ActiveLimit = 8;
    private int currentActiveLimit = 0;
   

    [Header("Scripts")]
    [SerializeField] private spawnEnemy spawn;
  

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        upgradeManager.instance.hasUpgraded = true;
        startWave();
    }
    public void onEnemyDeath()
    {
        enemiesAlive--;
        if (enemiesAlive < currentActiveLimit && enemiesSpawnedThisWave < maxEnemiesThisWave)
        {
            spawn.spawn();
        }
        else if (enemiesSpawnedThisWave >= maxEnemiesThisWave && enemiesAlive <= 0)
        {
            upgradeManager.instance.hasUpgraded = false;
            upgradeManager.instance.spawnUpgrades();
            
        }
    }
    public void startWave()
    {
        currentWave++;
       
        if (currentWave > totalWaves) { Debug.Log("You win!"); return; }
        enemiesSpawnedThisWave = 0;
        switch (currentWave)
        {
            case 1:
                currentActiveLimit = wave1ActiveLimit; break;
            case 2:
                currentActiveLimit = wave2ActiveLimit; break;
            case 3:
                currentActiveLimit = wave3ActiveLimit; break;
        }
        if(upgradeManager.instance.hasUpgraded) spawnEnemiesAtWaveStart();
    }
    private void spawnEnemiesAtWaveStart()
    {
        for (int i = 0; i < currentActiveLimit; i++)
        {
            Debug.Log("Spawning enemy " + i);
            spawn.spawn();
        }
    }
}