using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image fillRed;
    [SerializeField] private Image fillGreen;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private playerCombat combat;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercent = Mathf.Clamp01(player.Health / player.MaxHealth);
        fillRed.fillAmount = healthPercent;
        
        float coolDownPercent = Mathf.Clamp01(Time.time - player.lastAttackTime) / combat.attackCooldown;
        fillGreen.fillAmount = coolDownPercent;
        healthText.text = (int)player.Health + "/" + (int)player.MaxHealth;
    }
}
