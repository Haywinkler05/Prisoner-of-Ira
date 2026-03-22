using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Player Stats")]
    public float Health;
    public float MaxHealth;
    public float _Dmg;
    public float rageDamage;
    public float maxRage;
    public float lastAttackTime;
    public float rageDmgReduction;
    [SerializeField]private float rage;
    [SerializeField] private playerRage rageScript;
    [SerializeField] private playerCombat combat;
    [SerializeField] private AudioSource damageAudio;
   
    [SerializeField] private AudioClip damageClip;
  
    public float Rage
    {
        get { return rage; }
        set { rage = Mathf.Clamp(value, 0f, maxRage); }
    }
    public float damage { get { return _Dmg; } protected set { _Dmg = value; } }
    public float moveSpeed;
    public float damageRageBuildUp;
    public float sprintSpeed;
    public float healthRegenRate = 0.5f;
    public float regenDelay = 3.0f;
    private float lastDamageTime;
    [Header("Player components")]
    public GameObject player;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = MaxHealth;
        lastAttackTime = -combat.attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health < MaxHealth && Health > 0 && Time.time >= lastDamageTime + regenDelay)
        {
            Health = Mathf.Min(Health + healthRegenRate * Time.deltaTime, MaxHealth);
        }
    }
    public void OnAttack()
    {
        lastAttackTime = Time.time;
    }
    public void TakeDamage(float amount)
    {
        lastDamageTime = Time.time;
        float finalDamage = rageScript.enraged ? amount * rageDmgReduction : amount;
        Health -= amount;
        damageAudio.PlayOneShot(damageClip);
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
        rageScript.StopRage();
        anim.ResetTrigger("2_Attack");
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool)
                anim.SetBool(param.name, false);
            if (param.type == AnimatorControllerParameterType.Trigger)
                anim.ResetTrigger(param.name);
        }
        anim.SetTrigger("4_Death");
        anim.SetBool("isDeath", true);

        StartCoroutine(deathSequence());
    }

    private IEnumerator deathSequence()
    {
        // Stop all enemies from attacking
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (Enemy enemy in enemies)
        {
            enemy.enabled = false;
        }

        
        yield return new WaitForSeconds(1.5f);

        gameOver.instance.Show();
    }
}
