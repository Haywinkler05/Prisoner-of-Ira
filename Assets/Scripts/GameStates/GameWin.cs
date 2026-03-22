using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    public static GameWin instance;

    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI bestTimeText;
    public TextMeshProUGUI subtitleText;
    public CanvasGroup continueButtonGroup;
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
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(RevealSequence());
    }

    IEnumerator RevealSequence()
    {
        titleText.alpha = 0f;
        bestTimeText.alpha = 0f;
        subtitleText.alpha = 0f;
        continueButtonGroup.alpha = 0f;
        quitButtonGroup.alpha = 0f;
        continueButtonGroup.blocksRaycasts = false;
        quitButtonGroup.blocksRaycasts = false;

        yield return new WaitForSecondsRealtime(0.3f);
        yield return StartCoroutine(FadeInTMP(titleText, 0.6f));
        StartCoroutine(FlickerTitle());

        yield return new WaitForSecondsRealtime(0.2f);
        yield return StartCoroutine(FadeInTMP(bestTimeText, 0.5f));

        yield return new WaitForSecondsRealtime(0.2f);
        yield return StartCoroutine(FadeInTMP(subtitleText, 0.5f));

        yield return new WaitForSecondsRealtime(0.3f);
        yield return StartCoroutine(FadeInGroup(continueButtonGroup, 0.5f));
        yield return new WaitForSecondsRealtime(0.15f);
        yield return StartCoroutine(FadeInGroup(quitButtonGroup, 0.5f));

        continueButtonGroup.blocksRaycasts = true;
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

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}