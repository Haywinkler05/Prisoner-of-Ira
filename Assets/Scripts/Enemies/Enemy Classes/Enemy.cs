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
    [SerializeField] private float chaseStartDistance = 1.7f;
    public float enemyMoveSpeed;
    public enum EnemyDifficulty { Easy, Medium, Hard }
    public EnemyDifficulty difficulty;
    public float Health { get { return _health; } set { _health = value; } }
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public float Damage { get { return _damage; } set { _damage = value; } }
    public float MaxDamage { get { return _maxDamage; } set { _maxDamage = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    protected virtual void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = enemyStates.chase;
        nextAttackTime = Random.Range(0f, attackCooldown);
    }

    protected virtual void Update()
    {
        distToPlayer = Vector2.Distance(playerPos.position, enemy.transform.position);
        switch (currentState)
        {
            case enemyStates.chase:
                Vector2 playerDir = (playerPos.position - enemy.transform.position).normalized;
                Vector2 separationForce = Vector2.zero;
                Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, 1.5f, LayerMask.GetMask("enemy"));

                foreach (Collider2D nearby in nearbyEnemies)
                {
                    if (nearby.gameObject != gameObject)
                    {
                        Vector2 pushDir = (transform.position - nearby.transform.position).normalized;
                        float dist = Vector2.Distance(transform.position, nearby.transform.position);
                        separationForce += pushDir * (1.5f / Mathf.Max(dist, 0.1f));
                    }
                }

                Vector2 moveDir = (playerDir + separationForce).normalized;
                rb.linearVelocity = moveDir * enemyMoveSpeed;

                _animator.SetBool("1_Move", true);
                bool isMovingVertical = Mathf.Abs(moveDir.y) > 0.5f;
                _animator.SetFloat("animSpeed", isMovingVertical ? 1.5f : 1f);

                if (moveDir.x > 0)
                {
                    enemy.transform.localScale = new Vector3(-1, 1, 1);
                    attackPoint.localPosition = new Vector3(attackPointX, attackPointY, 0);
                }
                else if (moveDir.x < 0)
                {
                    enemy.transform.localScale = new Vector3(1, 1, 1);
                    attackPoint.localPosition = new Vector3(-attackPointX, attackPointY, 0);
                }

                if (distToPlayer <= attackStopDistance)
                {
                    rb.linearVelocity = Vector2.zero;
                    _animator.SetBool("1_Move", false);
                    currentState = enemyStates.attack;
                }
                break;

            case enemyStates.attack:
                rb.linearVelocity = Vector2.zero;

                if (distToPlayer >= chaseStartDistance)
                {
                    _animator.SetBool("1_Move", true);
                    currentState = enemyStates.chase;
                }
                else
                {
                    if (Time.time >= nextAttackTime)
                    {
                        _animator.SetTrigger("2_Attack");
                        nextAttackTime = Time.time + attackCooldown + Random.Range(0f, 0.5f);
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
        foreach (Collider2D hit in hitPlayer)
        {
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