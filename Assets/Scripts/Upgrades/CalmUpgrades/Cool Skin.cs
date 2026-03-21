using UnityEngine;

public class CoolSkin : Upgrades
{
    public override string upgradeName => "Cool Skin";

    public override string upgradeDescription => "Being imprisoned has taught you a few things, you're stronger when you're calm. However, its much harder to contain the rage";
    private float damageMutipler = 2f;
    private float rageMutipler = 2f;

    public override void Apply()
    {
        Player.modifyDamage(damageMutipler);
        combat.modifyRageBuildUp(rageMutipler);
    }
    


}
