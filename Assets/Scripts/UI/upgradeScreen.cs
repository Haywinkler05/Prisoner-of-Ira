using System.Collections;
using UnityEngine;

public class UpgradeScreen : MonoBehaviour
{
    public static UpgradeScreen instance;
    [SerializeField]private CanvasGroup canvasGroup;
    [SerializeField] private float fadeSpeed = 2f;

    void Awake()
    {
        instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(Fade(0f, 1f));
    }

    public void Hide()
    {
        StartCoroutine(FadeAndHide());
    }

    private IEnumerator Fade(float from, float to)
    {
        canvasGroup.alpha = from;
        while (Mathf.Abs(canvasGroup.alpha - to) > 0.01f)
        {
            canvasGroup.alpha = Mathf.MoveTowards(
                canvasGroup.alpha, to, fadeSpeed * Time.unscaledDeltaTime);
            yield return null;
        }
        canvasGroup.alpha = to;
    }

    private IEnumerator FadeAndHide()
    {
        yield return StartCoroutine(Fade(1f, 0f));
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}