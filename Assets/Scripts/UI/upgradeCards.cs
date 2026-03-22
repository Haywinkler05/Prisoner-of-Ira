using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class UpgradeCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    private Upgrades assignedUpgrade;
    private Vector3 originalPos;
    [SerializeField] private float hoverHeight = 20f;
    [SerializeField] private float hoverSpeed = 5f;
    private bool isHovering;
    [SerializeField] private AudioClip hoverSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = 0.5f;
        StartCoroutine(CaptureOriginalPos());
    }

    private System.Collections.IEnumerator CaptureOriginalPos()
    {
        yield return new WaitForEndOfFrame();
        originalPos = transform.localPosition;
    }
    void Update()
    {
        Vector3 targetPos = isHovering ?
            originalPos + Vector3.up * hoverHeight : originalPos;
        transform.localPosition = Vector3.Lerp(
            transform.localPosition, targetPos, Time.unscaledDeltaTime * hoverSpeed);
    }

    public void SetUpgrade(Upgrades upgrade)
    {
        assignedUpgrade = upgrade;
        nameText.text = upgrade.upgradeName;
        descriptionText.text = upgrade.upgradeDescription;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        if (hoverSound != null)
            audioSource.PlayOneShot(hoverSound);
    }
    public void OnPointerExit(PointerEventData eventData) => isHovering = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        upgradeManager.instance.processPickUp(assignedUpgrade);
    }
}