using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    [SerializeField] private float _maxDamage;
    [SerializeField] private float _moveSpeed;



    public float Health { get { return _health; } set {  _health = value; } }
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public float Damage { get { return _damage; } set { _damage = value; } }
    public float MaxDamage { get { return _maxDamage; } set { _maxDamage = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } } 


    public void TakeDamage(float incomingDamage)
    {
        _health -= incomingDamage;
        if (_health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        gameManager.instance.onEnemyDeath();
        Destroy(gameObject);
    }


}
