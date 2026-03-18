using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class rageBarUI : MonoBehaviour
{
    [Header("UI Parts")]
   [SerializeField] private Image rageBarBackground;
   [SerializeField] private Image rageBarFill;
   [SerializeField] private TextMeshProUGUI rageBarText;
  
    [Header("Scripts")]
    [SerializeField] private Player player;
    [SerializeField] private playerRage rage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        rageBarFill.fillAmount = player.Rage / player.maxRage;
        if (rage.cooldown)
        {
            rageBarText.gameObject.SetActive(true);
        }
        else
        {
            rageBarText.gameObject.SetActive(false);
        }
    }
}
