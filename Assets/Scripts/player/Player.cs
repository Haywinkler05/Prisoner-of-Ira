using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Player Stats")]
    public float Health;
    public float _Dmg;
    public float rageDamage;
    public float maxRage;
    public float lastAttackTime;
    public float rageDmgReduction;
    [SerializeField]private float rage;
    [SerializeField] private playerRage rageScript;
    public float Rage
    {
        get { return rage; }
        set { rage = Mathf.Clamp(value, 0f, maxRage); }
    }
    public float damage { get { return _Dmg; } protected set { _Dmg = value; } }
    public float moveSpeed;
    public float damageRageBuildUp;
    public float sprintSpeed;
    [Header("Player components")]
    public GameObject player;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAttack()
    {
        lastAttackTime = Time.time;
    }
    public void TakeDamage(float amount)
    {
        float finalDamage = rageScript.enraged ? amount * rageDmgReduction : amount;
        Health -= amount;
        if(!rageScript.cooldown && !rageScript.enraged)rage += damageRageBuildUp;
        if (Health <= 0f) {

            Die();
        }
    }
    public void modifyDamage(float amount)
    {
        damage *= amount;
    }
    public void modifyRageMutiplyer(float amount)
    {
        rageDamage *= amount;
    }
 
    private void Die()
    {
        anim.SetTrigger("4_Death");
        Debug.Log("Game Over");
    }
}
