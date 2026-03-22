using UnityEngine;
[CreateAssetMenu(fileName = "BloodRed", menuName = "Upgrades/BloodRed")]
public class BloodRed : Upgrades
{
    public override string upgradeName => "Blood Red";
    public override string upgradeDescription => "Rage builds slower, but enraged damage is doubled.";
    public override void Apply(Player player, playerCombat combat, playerRage rage, playerMovement movement)
    {
        combat.modifyRageBuildUp(0.5f);
        player.modifyRageMutiplyer(2f);
    }
}