using UnityEngine;
using UnityEngine.EventSystems;


public class buttonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Hover Settings")]
    public AudioClip hoverSound;
    public float hoverScale = 1.08f;
    public float scaleSpeed = 8f;

    private AudioSource audioSource;
    private Vector3 originalScale;
    private Vector3 targetScale;
    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = 0.5f;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.unscaledDeltaTime * scaleSpeed);
    
}

    public void OnPointerEnter(PointerEventData eventData)
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        if (cg != null && !cg.blocksRaycasts) return;

        targetScale = originalScale * hoverScale;
        if (hoverSound != null)
            audioSource.PlayOneShot(hoverSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }

}
