using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class rageBarUI : MonoBehaviour
{
    [Header("UI Images")]
    [SerializeField] private Image rageBarFill;
    [SerializeField] private Image chainOverlay;

    [Header("Sprites")]
    [SerializeField] private Sprite orangeFillcalmSprite;
    [SerializeField] private Sprite orangeFillRageSprite;
    [SerializeField] private Sprite blueFillSprite;
    [SerializeField] private Sprite chainsLocked;
    [SerializeField] private Sprite chainsBroken;
    [SerializeField] private Player player;
    [SerializeField] private playerRage rage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float fillRatio = player.Rage / player.maxRage;
        rageBarFill.fillAmount = fillRatio;

        if (rage.cooldown)
        {
            

            rageBarFill.sprite = blueFillSprite;
            rageBarFill.fillAmount = player.Rage / player.maxRage;
            chainOverlay.sprite = chainsLocked;
            rageBarFill.sprite = blueFillSprite;
            chainOverlay.sprite = chainsLocked;
        }
        else if(rage.enraged)
        {
            rageBarFill.sprite = orangeFillRageSprite;
            chainOverlay.sprite = chainsBroken;
            if(fillRatio < 0.9f)
            {
                chainOverlay.enabled = false;   
            }
        }
        else
        {
            chainOverlay.enabled = true;
            rageBarFill.sprite = orangeFillcalmSprite;
            chainOverlay.sprite = chainsLocked;
        }
    }
}
