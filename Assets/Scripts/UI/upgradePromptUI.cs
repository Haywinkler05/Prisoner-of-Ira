using UnityEngine;
using TMPro;
using System.Collections;

public class upgradePromptUI : MonoBehaviour
{
    public static upgradePromptUI instance;
    void Awake()
    {
        instance = this;
    }
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI promptText;

    public void ShowWaveAnnouncement(int waveNumber)
    {
        StopAllCoroutines();
        gameObject.SetActive(true);
        nameText.text = "Wave " + waveNumber;
        descriptionText.text = " Enemies incoming!";
        promptText.text = "";
        // Auto hide after a few seconds
        StartCoroutine(AutoHide());
    }

    private IEnumerator AutoHide()
    {
        yield return new WaitForSeconds(2f);
        Hide();
    }
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