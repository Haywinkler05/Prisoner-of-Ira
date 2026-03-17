
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
    [SerializeField] private float rageBuildUp;
    [SerializeField] private float rageMax = 1f;
    public bool enraged;
    public bool cooldown;
    [SerializeField] private rageState currentState;
    [Header("Enraged")]
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject playerOBJ;
    [SerializeField] private GameObject closestEnemy = null;
    [SerializeField] private float closestDist = Mathf.Infinity;
    [Header("Scripts")]
    [SerializeField] private Player player;
    [SerializeField] private playerMovement movement;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        playerOBJ = player.player;
    }

    // Update is called once per frame
    void Update()
    {
        rageBuildUp = player.Rage;
        switch (currentState)
        {
            case rageState.Normal:
                cooldown = false;
                enraged = false;
                closestDist = Mathf.Infinity;
                if (rageBuildUp >= rageMax)
                {
                    
                    Enraged();
                }
                break;
            case rageState.Enraged:
                if (closestEnemy != null)
                {
                    Vector2 enemyDir = closestEnemy.transform.position - playerOBJ.transform.position;
                    movement.Move(enemyDir);
                }
                break;
            case rageState.Cooldown:
                cooldown = true;
                break;
        }


    }

    private void Enraged()
    {
        currentState = rageState.Enraged;
        enraged = true;
        foreach (GameObject enemy in enemies) {
            float dist = Vector2.Distance(playerOBJ.transform.position, enemy.transform.position);
            if (dist < closestDist) { 
                closestDist = dist;
                closestEnemy = enemy;
            }
        }
        
    }
} 
