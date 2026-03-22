using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class gameOver : MonoBehaviour
{
    public static gameOver instance;

    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;
    public CanvasGroup restartButtonGroup;
    public CanvasGroup quitButtonGroup;

    [Header("Flicker Settings")]
    public float flickerMinInterval = 0.08f;
    public float flickerMaxInterval = 0.3f;
    public float flickerMinAlpha = 0.6f;

    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        if (MusicManager.instance != null) MusicManager.instance.PlayGameOver();
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(RevealSequence());
    }

    IEnumerator RevealSequence()
    {
        titleText.alpha = 0f;
        subtitleText.alpha = 0f;
        restartButtonGroup.alpha = 0f;
        quitButtonGroup.alpha = 0f;
        restartButtonGroup.blocksRaycasts = false;
        quitButtonGroup.blocksRaycasts = false;

        yield return new WaitForSecondsRealtime(0.3f);
        yield return StartCoroutine(FadeInTMP(titleText, 0.6f));
        StartCoroutine(FlickerTitle());

        yield return new WaitForSecondsRealtime(0.2f);
        yield return StartCoroutine(FadeInTMP(subtitleText, 0.5f));

        yield return new WaitForSecondsRealtime(0.3f);
        yield return StartCoroutine(FadeInGroup(restartButtonGroup, 0.5f));
        yield return new WaitForSecondsRealtime(0.15f);
        yield return StartCoroutine(FadeInGroup(quitButtonGroup, 0.5f));

        restartButtonGroup.blocksRaycasts = true;
        quitButtonGroup.blocksRaycasts = true;
    }

    IEnumerator FlickerTitle()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(flickerMinInterval, flickerMaxInterval));
            titleText.alpha = Random.Range(flickerMinAlpha, 1f);
            yield return new WaitForSecondsRealtime(0.05f);
            titleText.alpha = 1f;
        }
    }

    IEnumerator FadeInTMP(TextMeshProUGUI tmp, float duration)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / duration;
            tmp.alpha = Mathf.Clamp01(t);
            yield return null;
        }
    }

    IEnumerator FadeInGroup(CanvasGroup group, float duration)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / duration;
            group.alpha = Mathf.Clamp01(t);
            yield return null;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}