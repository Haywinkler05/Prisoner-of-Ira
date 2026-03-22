using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    private Upgrades assignedUpgrade;
    private Vector3 originalPos;
    [SerializeField] private float hoverHeight = 20f;
    [SerializeField] private float hoverSpeed = 5f;
    private bool isHovering;

    void Start()
    {
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

    public void OnPointerEnter(PointerEventData eventData) => isHovering = true;
    public void OnPointerExit(PointerEventData eventData) => isHovering = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        upgradeManager.instance.processPickUp(assignedUpgrade);
    }
}