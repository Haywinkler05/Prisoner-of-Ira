using TMPro;
using UnityEngine;

public class BloodRed : Upgrades
{
    public override string upgradeName => "Blood Red";
   

    public override string upgradeDescription => "As you have spent your years in prison, it has become harder to build that internal rage, but once you do, you are twice as terrifying as before";
    [SerializeField] private float ragePercent = 0.5f;
   

    public override void Apply()
    {
       combat.modifyRageBuildUp(ragePercent);
       Player.modifyRageMutiplyer(2f);
    }
   

}
