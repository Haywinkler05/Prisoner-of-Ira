using UnityEngine;

public class IronSkin : Upgrades
{
    public override string upgradeName => "Iron Skin";

    public override string upgradeDescription => "Nullifies rage from taking damage. Attack rage buildup doubled. +50% Health.";

    public override void Apply()
    {
        Player.damageRageBuildUp = 0f;
        combat.modifyRageBuildUp(2f);
        Player.Health *= 1.5f;
    }
     
    
}
