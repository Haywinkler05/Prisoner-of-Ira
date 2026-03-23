using UnityEngine;
[CreateAssetMenu(fileName = "BloodlustBlade", menuName = "Upgrades/BloodlustBlade")]
public class BloodlustBlade : Upgrades
{
    public override string upgradeName => "Bloodlust Blade";
    public override string upgradeDescription => "Attack damage +50%, attack cooldown reduced by 30%.";
    public override void Apply(Player player, playerCombat combat, playerRage rage, playerMovement movement)
    {
        player.modifyDamage(1.5f);
        combat.attackCooldown *= 0.7f;
    }
}