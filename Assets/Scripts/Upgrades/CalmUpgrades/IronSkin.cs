using UnityEngine;
[CreateAssetMenu(fileName = "IronSkin", menuName = "Upgrades/IronSkin")]
public class IronSkin : Upgrades
{
    public override string upgradeName => "Iron Skin";
    public override string upgradeDescription => "Nullifies rage from taking damage. Attack rage doubled. +50% Health.";
    public override void Apply(Player player, playerCombat combat, playerRage rage, playerMovement movement)
    {
        player.damageRageBuildUp = 0f;
        combat.modifyRageBuildUp(2f);
        player.Health *= 1.5f;
    }
}