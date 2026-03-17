using UnityEngine;

public class playerCombat : MonoBehaviour
{
    [Header("Attack Stats")]
    [SerializeField] private float nextAttackTime = 0f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float rageBuildUp = 0.3f;

    [Header("Scripts")]
    [SerializeField] private Player player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack()
    {
        if (Time.time >= nextAttackTime) {
            nextAttackTime = Time.time + attackCooldown;
            player.Rage += rageBuildUp;

        }
        //Check if attack cooldown is over
            //If it is, play the attack
            //update the rage bar in player
            //Check if the player's weapons hitbox hit an enemy
            //Check with combatManager to deal damage to enemies
        //Otherwise don't play attack
    }
}
