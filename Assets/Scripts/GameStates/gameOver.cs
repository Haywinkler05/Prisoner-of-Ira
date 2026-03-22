using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class gameOver : MonoBehaviour
{
    public static gameOver instance;
    private CanvasGroup canvasGroup;
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
        StartCoroutine(Fade(0f, 1f));
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