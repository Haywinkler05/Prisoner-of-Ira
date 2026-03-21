using System.Collections;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    [Header("Attack Stats")]
    [SerializeField] private float nextAttackTime = 0f;
    [SerializeField] private float attackAnimLength = 0.500f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float rageBuildUp = 0.3f;
    [Header("Detection")]
    public Transform attackPoint;
     public float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayer;
    [Header("Scripts")]
    [SerializeField] private Player player;
    [SerializeField] private playerRage rage;
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
            player.anim.SetTrigger("2_Attack");
            nextAttackTime = Time.time + attackCooldown;
            player.OnAttack();
            if(!rage.enraged && !rage.cooldown) player.Rage += rageBuildUp;
            StartCoroutine(preformHitCheck());
        }
    }
    private IEnumerator preformHitCheck()
    {
        yield return new WaitForSeconds(attackAnimLength);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            float finalDmg = rage.enraged ? player.rageDamage : player.damage;
            combatManager.Instance.requestDamage(enemy.gameObject, finalDmg);
        }
    }
    public void modifyRageBuildUp(float amount)
    {
        rageBuildUp *= amount;
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
