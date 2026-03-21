using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Image fillRed;
    [SerializeField] private Enemy enemy;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (enemy == null) Destroy(gameObject);
    }

    public void onDamageTaken()
    {
        float healthPercent = Mathf.Clamp01(enemy.Health / enemy.MaxHealth);
        fillRed.fillAmount = healthPercent;
    }
}
