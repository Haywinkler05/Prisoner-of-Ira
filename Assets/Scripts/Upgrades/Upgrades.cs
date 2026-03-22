
using UnityEngine;

public abstract class Upgrades : ScriptableObject, IUpgradable
{
    public abstract string upgradeName { get; }
    public abstract string upgradeDescription { get; }
    public abstract void Apply(Player player, playerCombat combat,
                               playerRage rage, playerMovement movement);
}