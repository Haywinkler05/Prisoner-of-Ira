using System.Collections;
using UnityEngine;

public enum enemyStates
{
    chase,
    attack
}
public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    [SerializeField] private float _maxDamage;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float nextAttackTime = 0f;
    [SerializeField] private float attackAnimLength = 0.500f;
    [SerializeField] private float attackCooldown = 1f;
    public float attackRange = 0.5f;
    private float distToPlayer;
    public Animator _animator;
    [SerializeField] private float _deathAnimLength = 0.667f;
    [SerializeField] private float attackStopDistance = 1.2f;
    [SerializeField] protected float attackPointX;
    [SerializeField] protected float attackPointY;
    protected enemyStates currentState;
    [SerializeField] protected GameObject enemy;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] private Transform playerPos;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private EnemyHealthUI healthUI;
    public float enemyMoveSpeed;



    public float Health { get { return _health; } set {  _health = value; } }
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public float Damage { get { return _damage; } set { _damage = value; } }
    public float MaxDamage { get { return _maxDamage; } set { _maxDamage = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
  
    protected virtual void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = enemyStates.chase;
    }
    protected virtual void Update()
    {
        distToPlayer = Vector2.Distance(playerPos.position, enemy.transform.position);
        switch (currentState)
        {
            case enemyStates.chase:
               Vector2 playerDir = (playerPos.position - enemy.transform.position).normalized;
                rb.linearVelocity = playerDir * enemyMoveSpeed;
                _animator.SetBool("1_Move", true);
                bool isMovingVertical = Mathf.Abs(playerDir.y) > 0.5f;
               _animator.SetFloat("animSpeed", isMovingVertical ? 1.5f : 1f);
               if(playerDir.x > 0) enemy.transform.localScale = new Vector3(-1,1, 1);
              if(playerDir.x < 0) enemy.transform.localScale = new Vector3(1,1,1);
              if (playerDir.x > 0) attackPoint.localPosition= new Vector3(attackPointX, attackPointY,0);
              if (playerDir.x < 0) attackPoint.localPosition = new Vector3(-attackPointX, attackPointY, 0);
                Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, 1f, LayerMask.GetMask("enemy"));
                foreach (Collider2D nearby in nearbyEnemies)
                {
                    if (nearby.gameObject != gameObject)
                    {
                        Vector2 pushDir = (transform.position - nearby.transform.position).normalized;
                        rb.AddForce(pushDir * 2f);
                    }
                }


                if (distToPlayer <= attackStopDistance)
                {
                    rb.linearVelocity = Vector2.zero;
                    _animator.SetBool("1_Move", false);
                    currentState = enemyStates.attack;

                }

                break;
            case enemyStates.attack:
               
                if (distToPlayer >= attackStopDistance) {
                    _animator.SetBool("1_Move", true);
                    currentState = enemyStates.chase;
                }
                else
                {
                    if(Time.time >= nextAttackTime)
                    {
                        _animator.SetTrigger("2_Attack");
                        nextAttackTime = Time.time + attackCooldown;
                        StartCoroutine(preformHitCheck());
                    }
                }
                    break;
        }
    }
    public void TakeDamage(float incomingDamage)
    {
        _health -= incomingDamage;
        healthUI.onDamageTaken();
        if (_health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        _animator.SetTrigger("4_Death");
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(death());
    }
    private IEnumerator death()
    {
        yield return new WaitForSeconds(_deathAnimLength);
        gameManager.instance.onEnemyDeath();
        Destroy(gameObject);
    }
    private IEnumerator preformHitCheck()
    {
        yield return new WaitForSeconds(attackAnimLength);
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D hit in hitPlayer) {
            combatManager.Instance.requestDamage(hit.gameObject, _damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
