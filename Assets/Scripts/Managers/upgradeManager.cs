using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradable
{
    string upgradeName { get; }
    string upgradeDescription { get; }
    void  Apply();
}

public class upgradeManager : MonoBehaviour
{
    public static upgradeManager instance;
    public List<GameObject> coolUpgrades;
    public List<GameObject> rageUpgrades;
    public Transform upgrade1Spawn;
    public Transform upgrade2Spawn;
    public bool hasUpgraded;
    private GameObject spawned1;
    private GameObject spawned2;
    private int lastCoolIndex;
    private int lastRageIndex;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void spawnUpgrades()
    {
        lastCoolIndex = Random.Range(0, coolUpgrades.Count);
        spawned1 = Instantiate(coolUpgrades[lastCoolIndex], upgrade1Spawn.position, Quaternion.identity);
        lastRageIndex = Random.Range(0, rageUpgrades.Count);
        spawned2 = Instantiate(rageUpgrades[lastRageIndex], upgrade2Spawn.position, Quaternion.identity);
    }
    public void processPickUp(GameObject chosenUpgrade)
    {
        hasUpgraded = true;
        if (chosenUpgrade == spawned1)
        {
            coolUpgrades.RemoveAt(lastCoolIndex);
            Destroy(spawned2);
            
        }
        else
        {
             rageUpgrades.RemoveAt(lastRageIndex);
            Destroy(spawned1);
        }
        gameManager.instance.startWave();
    }
}
