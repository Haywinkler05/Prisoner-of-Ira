using UnityEngine;
[CreateAssetMenu(fileName = "ChainBreaker", menuName = "Upgrades/ChainBreaker")]
public class ChainBreaker : Upgrades
{
    public override string upgradeName => "Chain Breaker";
    public override string upgradeDescription => "Rage cooldown reduced by 40%. Rage damage reduced by 25%.";
    public override void Apply(Player player, playerCombat combat, playerRage rage, playerMovement movement)
    {
        rage.coolDownDuration *= 0.6f;
        player.modifyRageMutiplyer(0.75f);
    }
}