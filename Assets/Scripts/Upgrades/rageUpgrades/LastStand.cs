using UnityEngine;
[CreateAssetMenu(fileName = "LastStand", menuName = "Upgrades/LastStand")]
public class LastStand : Upgrades
{
    public override string upgradeName => "Last Stand";
    public override string upgradeDescription => "Below 30% health, damage is tripled. Above 30%, damage is halved.";
    public override void Apply(Player player, playerCombat combat, playerRage rage, playerMovement movement)
    {
        combat.enableLastStand = true;
    }
}