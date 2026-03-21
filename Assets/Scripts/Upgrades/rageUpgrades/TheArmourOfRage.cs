using UnityEngine;

public class TheArmourOfRage : Upgrades
{
    public override string upgradeName => "The Armour of Rage";

    public override string upgradeDescription => "Your rage has become a barrier, and you are able to push through the pain. However, it is much harder to achieve this level of rage";

    public override void Apply()
    {
        Player.rageDmgReduction = 0f;
        combat.modifyRageBuildUp(0.5f);
    }

   
}
