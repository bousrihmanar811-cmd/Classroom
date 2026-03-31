//using UnityEngine;
//using TMPro;

//public class CountdownTimer : MonoBehaviour
//{
//    public int startMinutes = 10; // temps initial en minutes
//    private float remainingTime;

//    public TextMeshProUGUI timerText;

//    void Start()
//    {
//        remainingTime = startMinutes * 60; // convertir en secondes
//    }

//    void Update()
//    {
//        if (remainingTime > 0)
//        {
//            remainingTime -= Time.deltaTime;

//            int minutes = Mathf.FloorToInt(remainingTime / 60);
//            int seconds = Mathf.FloorToInt(remainingTime % 60);

//            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
//        }
//        else
//        {
//            remainingTime = 0;
//            timerText.text = "00:00";
//        }
//    }
//}


///////////////public Light environmentLight;


using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioSyncedCountdown : MonoBehaviour
{
    [Header("Références")]
    public AudioSource audioSource;            // Mets ton stress_timer_combined.mp3 ici
    public TextMeshProUGUI timerText;

    [Header("Affichage")]
    public int displayedSeconds = 60;          // Durée affichée (ex: 60s)
    public bool fitDisplayToBeatCount = false; // True = on affiche 1s par tic (durée = nb de tics)

    [Header("Détection de tics (analyse du clip)")]
    public bool analyzeClipOnStart = true;     // Analyse offline du clip avant playback
    [Range(0.1f, 5f)] public float thresholdStd = 1.5f; // Seuil = mean + std*coef
    [Range(0.05f, 0.5f)] public float minBeatIntervalSec = 0.18f; // anti-doublons
    public int analysisWindowSize = 1024;      // Taille fenêtre RMS

    [Header("Effets visuels")]
    public Color startColor = Color.white;
    public Color endColor = Color.red;
    public float maxScaleBoost = 0.1f;         // +10% à la fin

    [Header("Beats manuels (option)")]
    public bool useManualBeats = false;
    public List<float> manualBeatTimes = new List<float>(); // en secondes (dans l’audio)

    private List<float> beatTimes = new List<float>();
    private int nextBeatIndex = 0;
    private int timeLeft;                      // secondes affichées restantes
    private bool running = false;

    void Start()
    {
        if (audioSource == null || audioSource.clip == null || timerText == null)
        {
            Debug.LogError("Assigne audioSource.clip et timerText.");
            enabled = false; return;
        }

        // 1) Récupère les temps de tics
        if (useManualBeats)
        {
            beatTimes = new List<float>(manualBeatTimes);
            beatTimes.Sort();
        }
        else if (analyzeClipOnStart)
        {
            beatTimes = AnalyzeBeats(audioSource.clip, analysisWindowSize, thresholdStd, minBeatIntervalSec);
        }

        // 2) Initialise l’affichage
        if (fitDisplayToBeatCount && beatTimes.Count > 0)
            timeLeft = beatTimes.Count;     // 1 seconde d’affichage par tic
        else
            timeLeft = Mathf.Max(1, displayedSeconds);

        UpdateVisual(); // affiche l’état initial

        // 3) Lance l’audio + le suivi
        audioSource.Play();
        running = true;
    }

    void Update()
    {
        if (!running) return;

        // Si on a des beats, on décrémente pile au passage de chaque beat
        if (beatTimes.Count > 0)
        {
            float t = audioSource.time;
            while (nextBeatIndex < beatTimes.Count && t >= beatTimes[nextBeatIndex])
            {
                StepOneSecond();
                nextBeatIndex++;
                if (timeLeft <= 0) { StopAll(); break; }
            }
        }
        else
        {
            // Fallback : pas de beats détectés ? on suit la progression de l’audio
            float audioLen = audioSource.clip.length;
            float frac = Mathf.Clamp01(audioSource.time / audioLen);
            int target = Mathf.RoundToInt(Mathf.Lerp(displayedSeconds, 0, frac));
            if (target != timeLeft)
            {
                timeLeft = target;
                UpdateVisual();
                if (timeLeft <= 0) StopAll();
            }
        }
    }

    void StepOneSecond()
    {
        if (timeLeft <= 0) return;
        timeLeft = Mathf.Max(0, timeLeft - 1);
        UpdateVisual();
    }

    void StopAll()
    {
        running = false;
        timeLeft = 0;
        UpdateVisual();
        // Option : audioSource.Stop();
        // TODO: déclencher fin d’épreuve ici
    }

    void UpdateVisual()
    {
        int m = Mathf.FloorToInt(timeLeft / 60f);
        int s = timeLeft % 60;
        timerText.text = $"{m:00}:{s:00}";

        // Stress = progression (beats consommés / total)
        float progress;
        if (beatTimes.Count > 0)
        {
            int total = fitDisplayToBeatCount ? beatTimes.Count : displayedSeconds;
            int done = (fitDisplayToBeatCount ? beatTimes.Count - timeLeft
                                              : displayedSeconds - timeLeft);
            progress = (total > 0) ? Mathf.Clamp01(done / (float)total) : 1f;
        }
        else
        {
            // fallback sur progression audio
            progress = audioSource.clip.length > 0 ? Mathf.Clamp01(audioSource.time / audioSource.clip.length) : 1f;
        }

        timerText.color = Color.Lerp(startColor, endColor, progress);
        float scale = 1f + maxScaleBoost * progress;
        timerText.rectTransform.localScale = new Vector3(scale, scale, 1f);
    }

    // --- Analyse de beats (simple et robuste) ---
    List<float> AnalyzeBeats(AudioClip clip, int window, float stdK, float minGapSec)
    {
        var beats = new List<float>();
        int channels = clip.channels;
        int samples = clip.samples;
        int freq = clip.frequency;

        float[] raw = new float[samples * channels];
        clip.GetData(raw, 0);

        // 1) enveloppe RMS par fenêtre
        List<float> env = new List<float>();
        int step = window;
        for (int i = 0; i + window * channels <= raw.Length; i += step * channels)
        {
            double sum = 0;
            for (int j = 0; j < window; j++)
            {
                // mix en mono (moyenne des canaux)
                double v = 0;
                for (int c = 0; c < channels; c++)
                    v += raw[i + j * channels + c];
                v /= channels;
                sum += v * v;
            }
            float rms = Mathf.Sqrt((float)(sum / window));
            env.Add(rms);
        }

        if (env.Count < 3) return beats;

        // 2) seuil dynamique : mean + std*stdK
        float mean = 0f;
        for (int i = 0; i < env.Count; i++) mean += env[i];
        mean /= env.Count;

        float var = 0f;
        for (int i = 0; i < env.Count; i++)
        {
            float d = env[i] - mean;
            var += d * d;
        }
        float std = Mathf.Sqrt(var / env.Count);
        float thresh = mean + stdK * std;

        // 3) détection pics locaux + réfractaire
        int minGapFrames = Mathf.Max(1, Mathf.RoundToInt((minGapSec * freq) / window));
        int lastPeak = -minGapFrames;

        for (int i = 1; i < env.Count - 1; i++)
        {
            if (env[i] > thresh && env[i] > env[i - 1] && env[i] > env[i + 1] && (i - lastPeak) >= minGapFrames)
            {
                float timeSec = (i * window) / (float)freq;
                beats.Add(timeSec);
                lastPeak = i;
            }
        }

        return beats;
    }
}
