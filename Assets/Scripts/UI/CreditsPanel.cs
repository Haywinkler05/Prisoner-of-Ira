using System.Collections;
using UnityEngine;

public class CreditsPanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField] private float fadeSpeed = 2f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
       
    }

    public void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        StartCoroutine(Fade(0f, 1f));
    }

    public void Hide()
    {
        StartCoroutine(FadeAndHide());
    }

    private IEnumerator Fade(float from, float to)
    {
        canvasGroup.alpha = from;
        canvasGroup.blocksRaycasts = true;

        while (Mathf.Abs(canvasGroup.alpha - to) > 0.01f)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, to, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        canvasGroup.alpha = to;
    }

    private IEnumerator FadeAndHide()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        yield return StartCoroutine(Fade(1f, 0f));
        gameObject.SetActive(false);
    }
}
