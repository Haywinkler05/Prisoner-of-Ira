using UnityEngine;
[CreateAssetMenu(fileName = "TheArmourOfRage", menuName = "Upgrades/TheArmourOfRage")]
public class TheArmourOfRage : Upgrades
{
    public override string upgradeName => "The Armour of Rage";
    public override string upgradeDescription => "Rage nullifies damage taken, but rage builds at half speed.";
    public override void Apply(Player player, playerCombat combat, playerRage rage, playerMovement movement)
    {
        player.rageDmgReduction = 0f;
        combat.modifyRageBuildUp(0.5f);
    }
}