using UnityEngine;
[CreateAssetMenu(fileName = "DungeonResilience", menuName = "Upgrades/DungeonResilience")]
public class DungeonResilience : Upgrades
{
    public override string upgradeName => "Dungeon Resilience";
    public override string upgradeDescription => "Max health +50. Regen kicks in twice as fast.";
    public override void Apply(Player player, playerCombat combat, playerRage rage, playerMovement movement)
    {
        player.MaxHealth += 50f;
        player.Health += 50f;
        player.regenDelay *= 0.5f;
    }
}