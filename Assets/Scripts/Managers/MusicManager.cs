using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource gameAmbientSource;
    [SerializeField] private AudioSource upgradeSource;
    [SerializeField] private AudioSource gameOverSource;
    [SerializeField] private AudioSource gameWinSource;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer gameMixer;

    [Header("Transition Settings")]
    [SerializeField] private float crossfadeDuration = 2f;
    [SerializeField] private float rageTransitionTime = 3.5f;

    [Header("Rage Effect Settings")]
    [SerializeField] private float maxDistortion = 0.2f;
    [SerializeField] private float minPitch = 0.9f;
    [SerializeField] private float maxPitch = 1.15f;

    [Header("Scripts")]
    [SerializeField] private Player player;
    [SerializeField] private playerRage rage;

    private bool _gameEnded = false;
    private bool _inUpgrade = false;

    public float TargetRage { get; set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Start all sources silent except game ambient
        gameAmbientSource.volume = 0f;
        gameAmbientSource.Play();
        StartCoroutine(FadeSource(gameAmbientSource, 1f, crossfadeDuration));

        if (upgradeSource != null) { upgradeSource.volume = 0f; upgradeSource.Play(); }
        if (gameOverSource != null) { gameOverSource.volume = 0f; gameOverSource.Play(); }
        if (gameWinSource != null) { gameWinSource.volume = 0f; gameWinSource.Play(); }

        gameMixer.SetFloat("RageDistortion", 0f);
        gameMixer.SetFloat("RagePitch", 1.0f);
    }

    private void Update()
    {
        if (_gameEnded || _inUpgrade) return;

        float normalizedRage = Mathf.Clamp01(player.Rage / player.maxRage);
        UpdateMixerEffects(normalizedRage);
    }

    private void UpdateMixerEffects(float normalizedRage)
    {
        float distortionT = Mathf.Clamp01((normalizedRage - 0.3f) / 0.7f);
        float targetDistortion = rage.enraged ? maxDistortion : Mathf.Lerp(0f, maxDistortion * 0.5f, distortionT);
        float targetPitch = rage.enraged ? maxPitch : Mathf.Lerp(1.0f, minPitch, distortionT);

        gameMixer.GetFloat("RageDistortion", out float currentDist);
        gameMixer.GetFloat("RagePitch", out float currentPitch);

        gameMixer.SetFloat("RageDistortion", Mathf.MoveTowards(currentDist, targetDistortion, Time.deltaTime / rageTransitionTime));
        gameMixer.SetFloat("RagePitch", Mathf.MoveTowards(currentPitch, targetPitch, Time.deltaTime / rageTransitionTime));

        // Duck volume slightly during distortion
        float targetVol = Mathf.Lerp(1f, 0.7f, targetDistortion / maxDistortion);
        gameAmbientSource.volume = Mathf.MoveTowards(gameAmbientSource.volume, targetVol, Time.deltaTime / rageTransitionTime);
    }

    public void EnterUpgradeMusic()
    {
        _inUpgrade = true;
        ResetMixerEffects();
        StartCoroutine(CrossfadeTo(upgradeSource, gameAmbientSource));
    }

    public void ExitUpgradeMusic()
    {
        _inUpgrade = false;
        StartCoroutine(CrossfadeTo(gameAmbientSource, upgradeSource));
    }

    public void PlayGameOver()
    {
        _gameEnded = true;
        _inUpgrade = false;
        StopAllCoroutines();
        StartCoroutine(ResetMixerCoroutine());
        StartCoroutine(CrossfadeTo(gameOverSource, gameAmbientSource));
    }

    public void PlayGameWin()
    {
        _gameEnded = true;
        _inUpgrade = false;
        StopAllCoroutines();
        StartCoroutine(ResetMixerCoroutine());
        StartCoroutine(CrossfadeTo(gameWinSource, gameAmbientSource));
    }
    private void ResetMixerEffects()
    {
        StopAllCoroutines();
        StartCoroutine(ResetMixerCoroutine());
    }

    private IEnumerator ResetMixerCoroutine()
    {
        float t = 0f;
        gameMixer.GetFloat("RageDistortion", out float startDist);
        gameMixer.GetFloat("RagePitch", out float startPitch);

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / crossfadeDuration;
            gameMixer.SetFloat("RageDistortion", Mathf.Lerp(startDist, 0f, t));
            gameMixer.SetFloat("RagePitch", Mathf.Lerp(startPitch, 1f, t));
            yield return null;
        }
    }

    private IEnumerator CrossfadeTo(AudioSource fadeIn, AudioSource fadeOut)
    {
        float t = 0f;
        float startFadeOut = fadeOut.volume;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / crossfadeDuration;
            fadeIn.volume = Mathf.Clamp01(t);
            fadeOut.volume = Mathf.Lerp(startFadeOut, 0f, t);
            yield return null;
        }

        fadeOut.volume = 0f;
        fadeIn.volume = 1f;
    }

    private IEnumerator FadeSource(AudioSource source, float targetVolume, float duration)
    {
        float t = 0f;
        float start = source.volume;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / duration;
            source.volume = Mathf.Lerp(start, targetVolume, t);
            yield return null;
        }
        source.volume = targetVolume;
    }
}