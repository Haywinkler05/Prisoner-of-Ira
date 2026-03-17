using UnityEngine;
using UnityEngine.UI;

public class rageBarUI : MonoBehaviour
{
    [Header("UI Parts")]
   [SerializeField] private Image rageBarBackground;
   [SerializeField] private Image rageBarFill;
  
    [Header("Scripts")]
    [SerializeField] private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        rageBarFill.fillAmount = player.Rage;
    }
}
