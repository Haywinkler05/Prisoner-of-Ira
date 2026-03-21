using UnityEngine;

public class IronSkin : Upgrades
{
    public override string upgradeName => "Iron Skin";

    public override string upgradeDescription => "Your years of keeping your rage composed completely nullifes rage build up from others and you have become a lot tougher. However, attacking others reminds you of what you once were, and your rage doubles as you attack.";

    public override void Apply()
    {
        Player.damageRageBuildUp = 0f;
        combat.modifyRageBuildUp(2f);
        Player.Health *= 1.5f;
    }
     
    
}
