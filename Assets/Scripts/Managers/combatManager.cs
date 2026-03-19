using UnityEngine;
public interface IDamageable
{
    void TakeDamage(float amount);
}
public class combatManager : MonoBehaviour
{
    public static combatManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void requestDamage(GameObject target, float damage)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
