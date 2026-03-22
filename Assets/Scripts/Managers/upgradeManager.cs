using System.Collections.Generic;
using UnityEngine;

public interface IUpgradable
{
    string upgradeName { get; }
    string upgradeDescription { get; }
    void Apply(Player player, playerCombat combat, playerRage rage, playerMovement movement);
}

public class upgradeManager : MonoBehaviour
{
    public static upgradeManager instance;
    public List<Upgrades> coolUpgrades;
    public List<Upgrades> rageUpgrades;
    public bool hasUpgraded;

    [Header("UI")]
    [SerializeField] private UpgradeScreen upgradeScreen;
    [SerializeField] private UpgradeCardUI card1;
    [SerializeField] private UpgradeCardUI card2;

    [Header("Player References")]
    private Player player;
    private playerCombat combat;
    private playerRage rage;
    private playerMovement movement;

    private Upgrades currentCoolUpgrade;
    private Upgrades currentRageUpgrade;
    private int lastCoolIndex;
    private int lastRageIndex;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<Player>();
            combat = playerObj.GetComponentInChildren<playerCombat>();
            rage = playerObj.GetComponentInChildren<playerRage>();
            movement = playerObj.GetComponentInChildren<playerMovement>();
        }
    }

    public void spawnUpgrades()
    {
        lastCoolIndex = Random.Range(0, coolUpgrades.Count);
        currentCoolUpgrade = coolUpgrades[lastCoolIndex];

        lastRageIndex = Random.Range(0, rageUpgrades.Count);
        currentRageUpgrade = rageUpgrades[lastRageIndex];

        card1.SetUpgrade(currentRageUpgrade);
        card2.SetUpgrade(currentCoolUpgrade);

        upgradeScreen.Show();
    }

    public void processPickUp(Upgrades chosenUpgrade)
    {
        hasUpgraded = true;
        chosenUpgrade.Apply(player, combat, rage, movement);

        if (chosenUpgrade == currentCoolUpgrade)
            coolUpgrades.RemoveAt(lastCoolIndex);
        else
            rageUpgrades.RemoveAt(lastRageIndex);

        upgradeScreen.Hide();
        gameManager.instance.startWave();
    }
}