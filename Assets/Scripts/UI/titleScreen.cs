using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class titleScreen : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;
    public CanvasGroup startButtonGroup;
    public CanvasGroup creditsButtonGroup;
    public CanvasGroup quitButtonGroup;
    public CanvasGroup fadeGroup;

    [Header("Flicker Settings")]
    public float flickerMinInterval = 0.08f;
    public float flickerMaxInterval = 0.3f;
    public float flickerMinAlpha = 0.6f;

    [Header("Pulse Settings")]
    public float pulseSpeed = 1.2f;
    public float pulseMin = 0.97f;
    public float pulseMax = 1.03f;

    private void Start()
    {
        subtitleText.GetComponent<CanvasGroup>().alpha = 0f;
        startButtonGroup.alpha = 0f;
        creditsButtonGroup.alpha = 0f;
        quitButtonGroup.alpha = 0f;
        startButtonGroup.blocksRaycasts = false;
        creditsButtonGroup.blocksRaycasts = false;
        quitButtonGroup.blocksRaycasts = false;

        StartCoroutine(FlickerTitle());
        StartCoroutine(pulseTitle());
        StartCoroutine(FadeInSequence());

    }
   
    IEnumerator FlickerTitle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(flickerMinInterval, flickerMaxInterval));
            titleText.alpha = Random.Range(flickerMinAlpha, 1f);
            yield return new WaitForSeconds(0.05f);
            titleText.alpha = 1f;


        }
    } 

    IEnumerator pulseTitle()
    {
        while (true)
        {
            float t = Mathf.PingPong(Time.time * pulseSpeed, 1f);
            float scale = Mathf.Lerp(pulseMin, pulseMax, t);
            titleText.transform.localScale = Vector3.one * scale;
            yield return null;
        }
    }
    IEnumerator FadeInSequence()
    {
        yield return new WaitForSeconds(0.8f);
        yield return StartCoroutine(FadeIn(subtitleText.GetComponent<CanvasGroup>(), 1.5f));

        // Buttons fade in one by one
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(FadeIn(startButtonGroup, 0.8f));
        startButtonGroup.blocksRaycasts = true;
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(FadeIn(creditsButtonGroup, 0.8f));
        creditsButtonGroup.blocksRaycasts = true;
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(FadeIn(quitButtonGroup, 0.8f));
        quitButtonGroup.blocksRaycasts = true;

    }
    IEnumerator FadeIn(CanvasGroup group, float duration)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            group.alpha = Mathf.Clamp01(t);
            yield return null;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator FadeAndLoad(int sceneIndex)
    {
        // Disable button interaction immediately
        startButtonGroup.interactable = false;
        creditsButtonGroup.interactable = false;
        quitButtonGroup.interactable = false;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 0.8f;
            fadeGroup.alpha = Mathf.Clamp01(t);
            yield return null;
        }

        // Make sure we're fully black before loading
        fadeGroup.alpha = 1f;
        yield return new WaitForSeconds(0.1f);
      
    }
}
