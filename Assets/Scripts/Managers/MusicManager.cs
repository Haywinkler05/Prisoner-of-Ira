using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] private AudioSource ambientSource;
    [SerializeField] private AudioSource distortionSource;
    [SerializeField] private AudioSource heavySource;

    [Header("Timings")]
    [SerializeField] private float transitionTime = 0.6f;
    [SerializeField] private float heavyDropTimestamp = 4.0f;
    [Header("Scripts")]
    [SerializeField] private Player player;
    [SerializeField] private playerRage rage;
    private float _fadeVelocityAmbient;
    private float _fadeVelocityDistortion;
    private float _fadeVelocityHeavy;

    public float TargetRage { get; set; } 
    private bool _isRaging;
    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        ambientSource.Play();
        distortionSource.Play();
        distortionSource.volume = 0;

        heavySource.Stop();
    }
    private void Update()
    {
        if (rage.enraged && !heavySource.isPlaying) {
            if(heavySource.time < heavyDropTimestamp)
            {
                heavySource.time = heavyDropTimestamp;
            }
           
            heavySource.Play();
        }

        float normalizedRage = Mathf.Clamp01(player.Rage / player.maxRage);

        UpdateVolumes(normalizedRage);
    }
    private void UpdateVolumes(float normalizedRage)
    {
        
        float targetAmb = rage.enraged ? 0.2f : 1.0f;

        
        float targetDist = (rage.enraged || normalizedRage > 0.5f) ? 0.7f : 0.0f;

        float targetHeavy = rage.enraged ? 1.0f : 0.0f;

    
        ambientSource.volume = Mathf.SmoothDamp(ambientSource.volume, targetAmb, ref _fadeVelocityAmbient, transitionTime);
        distortionSource.volume = Mathf.SmoothDamp(distortionSource.volume, targetDist, ref _fadeVelocityDistortion, transitionTime);
        heavySource.volume = Mathf.SmoothDamp(heavySource.volume, targetHeavy, ref _fadeVelocityHeavy, transitionTime);


        if (!rage.enraged && heavySource.volume < 0.01f && heavySource.isPlaying)
        {
            heavySource.Pause();
        }
    }
}
