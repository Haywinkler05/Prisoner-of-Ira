using UnityEngine;
[CreateAssetMenu(fileName = "WrathOfIra", menuName = "Upgrades/WrathOfIra")]
public class WrathOfIra : Upgrades
{
    public override string upgradeName => "Wrath of Ira";
    public override string upgradeDescription => "Rage fills instantly on kill. Cooldown doubled.";
    public override void Apply(Player player, playerCombat combat, playerRage rage, playerMovement movement)
    {
        rage.coolDownDuration *= 2f;
        rage.fillOnKill = true;
    }
}