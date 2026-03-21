
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
public enum rageState
{
    Normal, 
    Enraged, 
    Cooldown
}
public class playerRage : MonoBehaviour
{
    [Header("Rage Stats")]
    [SerializeField] private float rage;
    [SerializeField] private float rageDecayRate = 0.1f;
    [SerializeField] private float rageDrainPerSecond = 0.33f;
    [SerializeField] private float coolDownDuration = 5f;
    public bool enraged;
    public bool cooldown;
    [SerializeField] private rageState currentState;
    [Header("Enraged")]
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject playerOBJ;
    [SerializeField] private GameObject closestEnemy = null;
    [SerializeField] private float attackStopDistance = 1.2f;
    [SerializeField] private float closestDist = Mathf.Infinity;
    [Header("Scripts")]
    [SerializeField] private Player player;
    [SerializeField] private playerCombat combat;
    [SerializeField] private playerMovement movement;

    void Start()
    {
        playerOBJ = player.player;
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (currentState)
        {
            case rageState.Normal:
                if (Time.time > player.lastAttackTime + 2f)
                {
                    player.Rage -= rageDecayRate * Time.deltaTime;
                }
                closestDist = Mathf.Infinity;
                if (player.Rage >= player.maxRage)
                { 
                    Enraged();
                }
                break;
            case rageState.Enraged:
                break;
            case rageState.Cooldown:
                break;
        }


    }
    private void FindClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        closestDist = Mathf.Infinity;
        closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(playerOBJ.transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestEnemy = enemy;
            }
        }
    }
    private void Enraged()
    {
        currentState = rageState.Enraged;
        enraged = true;
        FindClosestEnemy();
        float dynamicDuration = player.maxRage / rageDrainPerSecond;
        StartCoroutine(rageActive(dynamicDuration));

    }
    IEnumerator rageActive(float duration)
    {
        float elapsedTime = 0f;
        float startingRage = player.maxRage;
        yield return new WaitForSeconds(0.5f);
        while (elapsedTime < duration) {
            player.Rage = Mathf.Lerp(startingRage, 0f, elapsedTime / duration);
            if (closestEnemy != null)
            {
                float distToEnemy = Vector2.Distance(player.player.transform.position, closestEnemy.transform.position);
                if (distToEnemy <= attackStopDistance)
                {
                    Vector2 enemyDir = (closestEnemy.transform.position - playerOBJ.transform.position).normalized;
                    combat.attackPoint.localPosition = enemyDir * combat.attackRange;
                    movement.Move(Vector2.zero);
                    combat.Attack();
                }
                else
                {
                    Vector2 enemyDir = (closestEnemy.transform.position - playerOBJ.transform.position).normalized;
                    combat.attackPoint.localPosition = enemyDir * combat.attackRange;
                    movement.Move(enemyDir);
                }
                   
            }
            else if(closestEnemy == null) 
            {
                FindClosestEnemy() ;
            }
           

                elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.Rage = 0f;
        StartCoroutine(cooldownRoutine());
    }

    IEnumerator cooldownRoutine()
    {
        currentState = rageState.Cooldown;
        enraged = false;
        cooldown = true;
        float elapsedTime = 0f;
        float startRage = player.maxRage;
        while(elapsedTime < coolDownDuration)
        {
            elapsedTime += Time.deltaTime;
            player.Rage = Mathf.Lerp(startRage, 0f, elapsedTime / coolDownDuration);
            yield return null;
        }

        player.Rage = 0f;
        cooldown = false;
        currentState = rageState.Normal;
    }
} 
