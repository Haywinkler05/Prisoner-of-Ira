using UnityEngine;

public abstract class Upgrades : MonoBehaviour, IUpgradable
{
    public abstract string upgradeName { get; }
    public abstract string upgradeDescription { get; }

    protected  bool upgradingRage;
    protected bool downgradingRage;
    [Header("Scripts")]
    [SerializeField]protected Player Player;
    [SerializeField] protected playerCombat combat;
    [SerializeField] protected playerRage rage;
    [SerializeField] protected playerMovement movement;

    public abstract void Apply();

    
}
