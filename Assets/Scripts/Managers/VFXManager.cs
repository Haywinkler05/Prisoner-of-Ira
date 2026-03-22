using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;

    [Header("Volume")]
    [SerializeField] private Volume postVolume;

    [Header("Vignette Settings")]
    [SerializeField] private float vignetteBaseIntensity = 0.3f;
    [SerializeField] private float vignetteRageIntensity = 0.6f;
    [SerializeField] private float vignettePulseSpeed = 2f;

    [Header("Chromatic Aberration Settings")]
    [SerializeField] private float maxChromaticIntensity = 0.8f;

    [Header("Scripts")]
    [SerializeField] private Player player;
    [SerializeField] private playerRage rage;

    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;

    private float targetVignette;
    private float targetChromatic;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        postVolume.profile.TryGet<Vignette>(out vignette);
        postVolume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
        postVolume.profile.TryGet<LensDistortion>(out lensDistortion);
    }

    void Update()
    {
        float normalizedRage = Mathf.Clamp01(player.Rage / player.maxRage);
        UpdateVignette(normalizedRage);
        UpdateChromaticAberration(normalizedRage);
    }

    private void UpdateVignette(float normalizedRage)
    {
        if (rage.enraged)
        {
            float pulse = Mathf.PingPong(Time.time * vignettePulseSpeed, 1f);
            targetVignette = Mathf.Lerp(vignetteRageIntensity, vignetteRageIntensity + 0.2f, pulse);
        }
        else
        {
            targetVignette = Mathf.Lerp(vignetteBaseIntensity, vignetteRageIntensity, normalizedRage);
        }

        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, targetVignette, Time.deltaTime * 3f);
    }

    private void UpdateChromaticAberration(float normalizedRage)
    {
        float chromaticT = Mathf.Clamp01((normalizedRage - 0.5f) / 0.5f);
        targetChromatic = rage.enraged ? maxChromaticIntensity : Mathf.Lerp(0f, maxChromaticIntensity * 0.5f, chromaticT);
        chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, targetChromatic, Time.deltaTime * 3f);
    }

    public void OnHit()
    {
        StartCoroutine(HitFlash());
    }

    private System.Collections.IEnumerator HitFlash()
    {
        chromaticAberration.intensity.value = 1f;
        lensDistortion.intensity.value = -20f;
        yield return new WaitForSeconds(0.1f);
        chromaticAberration.intensity.value = targetChromatic;
        lensDistortion.intensity.value = 0f;
    }
}