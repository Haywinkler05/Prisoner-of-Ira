using UnityEngine;
[CreateAssetMenu(fileName = "CoolSkin", menuName = "Upgrades/CoolSkin")]
public class CoolSkin : Upgrades
{
    public override string upgradeName => "Cool Skin";
    public override string upgradeDescription => "+2x damage, but rage builds twice as fast.";
    private float damageMutipler = 2f;
    private float rageMutipler = 2f;
    public override void Apply(Player player, playerCombat combat, playerRage rage, playerMovement movement)
    {
        player.modifyDamage(damageMutipler);
        combat.modifyRageBuildUp(rageMutipler);
    }
}