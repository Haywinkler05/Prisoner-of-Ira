using TMPro;
using UnityEngine;

public class BloodRed : Upgrades
{
    public override string upgradeName => "Blood Red";
   

    public override string upgradeDescription => "Rage builds slower, but enraged damage is doubled";
    [SerializeField] private float ragePercent = 0.5f;
   

    public override void Apply()
    {
       combat.modifyRageBuildUp(ragePercent);
       Player.modifyRageMutiplyer(2f);
    }
   

}
