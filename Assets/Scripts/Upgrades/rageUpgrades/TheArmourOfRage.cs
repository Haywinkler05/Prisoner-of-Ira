using UnityEngine;

public class TheArmourOfRage : Upgrades
{
    public override string upgradeName => "The Armour of Rage";

    public override string upgradeDescription => "Rage nullifies damage taken, but rage builds at half speed.";

    public override void Apply()
    {
        Player.rageDmgReduction = 0f;
        combat.modifyRageBuildUp(0.5f);
    }

   
}
