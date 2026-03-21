using TMPro;
using UnityEngine;

public abstract class Upgrades : MonoBehaviour, IUpgradable
{
    public abstract string upgradeName { get; }
    public abstract string upgradeDescription { get; }

    protected  bool upgradingRage;
    protected bool downgradingRage;
    public static Upgrades currentUpgrade;
    [Header("Scripts")]
    [SerializeField]protected Player Player;
    [SerializeField] protected playerCombat combat;
    [SerializeField] protected playerRage rage;
    [SerializeField] protected playerMovement movement;
    [SerializeField] private upgradeManager upgradeManager;
    
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            // Link all the required components automatically
            Player = playerObj.GetComponent<Player>();
            combat = playerObj.GetComponentInChildren<playerCombat>();
            rage = playerObj.GetComponentInChildren<playerRage>();
            movement = playerObj.GetComponentInChildren<playerMovement>();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            currentUpgrade = this;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            currentUpgrade = null;
        }
    }
    public abstract void Apply();

    public void upgradePickUp()
    {
        Apply();
        upgradeManager.instance.processPickUp(this.gameObject);
        Destroy(gameObject);
    }
}
