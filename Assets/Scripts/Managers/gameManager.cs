using TMPro;
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

    [Header("Scaling Buffs")]
    [SerializeField] private int healthBuffPerWave = 15;

    [Header("Win UI Slots")]
    public GameObject gameWinPanel;
    public TextMeshProUGUI timeText;

    [Header("Scripts")]
    [SerializeField] private Player player;
    [SerializeField] private playerRage rage;
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
            if (currentWave >= totalWaves)
            {
                TriggerWin();
            }
            else
            {
                if (MusicManager.instance != null) MusicManager.instance.TargetRage = 0f;
                upgradeManager.instance.hasUpgraded = false;
                upgradeManager.instance.spawnUpgrades();
            }
        }
    }

    public void startWave()
    {
        currentWave++;

        if (currentWave > totalWaves) return;

        if (currentWave > 1)
        {
            rage.ResetRage();
            player.MaxHealth += healthBuffPerWave;
            player.Health = player.MaxHealth;
        }

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

        if (upgradeManager.instance.hasUpgraded) spawnEnemiesAtWaveStart();
    }

    private void spawnEnemiesAtWaveStart()
    {
        for (int i = 0; i < currentActiveLimit; i++)
        {
            spawn.spawn();
        }
    }

    public void TriggerWin()
    {
        gameWinPanel.SetActive(true);

        if (EscapeTimer.instance != null)
        {
            EscapeTimer.instance.StopTimer();
            timeText.text = EscapeTimer.instance.GetFinalTime();
        }

        Time.timeScale = 0f;

        if (MusicManager.instance != null) MusicManager.instance.TargetRage = 0f;
    }
}