using UnityEngine;
using TMPro;
using System;

public class EscapeTimer : MonoBehaviour
{
    public static EscapeTimer instance;
    [SerializeField] private TextMeshProUGUI timerText;
    private float startTime;
    private bool isRunning;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        startTime = Time.time;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            float t = Time.time - startTime;
            TimeSpan time = TimeSpan.FromSeconds(t);
            timerText.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public string GetFinalTime()
    {
        return timerText.text;
    }
}