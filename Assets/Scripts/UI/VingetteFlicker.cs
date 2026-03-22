using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VignetteFlicker : MonoBehaviour
{
    private Image vignetteImage;
    public float flickerSpeed = 2f;
    public float minAlpha = 0.25f;
    public float maxAlpha = 0.4f;

    void Start()
    {
        vignetteImage = GetComponent<Image>();
        StartCoroutine(FlickerVignette());
    }

    IEnumerator FlickerVignette()
    {
        while (true)
        {
            float t = Mathf.PingPong(Time.time * flickerSpeed, 1f);
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);
            vignetteImage.color = new Color(0f, 0f, 0f, alpha);

            // Occasional random flicker
            if (Random.Range(0f, 1f) < 0.02f)
            {
                vignetteImage.color = new Color(0f, 0f, 0f, maxAlpha);
                yield return new WaitForSeconds(0.05f);
            }

            yield return null;
        }
    }
}