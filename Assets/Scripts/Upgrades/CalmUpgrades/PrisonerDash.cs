using UnityEngine;
[CreateAssetMenu(fileName = "PrisonersDash", menuName = "Upgrades/PrisonersDash")]
public class PrisonersDash : Upgrades
{
    public override string upgradeName => "Prisoner's Dash";
    public override string upgradeDescription => "Sprint speed doubled, but health regen stops.";
    public override void Apply(Player player, playerCombat combat, playerRage rage, playerMovement movement)
    {
        player.sprintSpeed *= 2f;
        player.healthRegenRate = 0f;
    }
}