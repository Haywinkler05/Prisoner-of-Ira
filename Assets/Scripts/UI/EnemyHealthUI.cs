using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Image fillRed;
    [SerializeField] private Enemy enemy;
    private Canvas canvas;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        gameObject.SetActive(true);
        
        float healthPercent = Mathf.Clamp01(enemy.Health / enemy.MaxHealth);
        Debug.Log("Health: " + enemy.Health + " Max: " + enemy.MaxHealth + " Percent: " + healthPercent);
        fillRed.fillAmount = healthPercent;
        Canvas.ForceUpdateCanvases();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }
        float healthPercent = Mathf.Clamp01(enemy.Health / enemy.MaxHealth);
        fillRed.fillAmount = healthPercent;
    }

    public void onDamageTaken()
    {
        gameObject.SetActive(true);
        float healthPercent = Mathf.Clamp01(enemy.Health / enemy.MaxHealth);
        fillRed.fillAmount = healthPercent;
        Canvas.ForceUpdateCanvases();
    }
}