using UnityEngine;
using TMPro;

public class upgradePromptUI : MonoBehaviour
{
    public static upgradePromptUI instance;
    void Awake()
    {
        instance = this;
    }
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(string upgradeName, string upgradeDescription)
    {
        gameObject.SetActive(true);
        nameText.text = upgradeName;
        descriptionText.text = upgradeDescription;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}