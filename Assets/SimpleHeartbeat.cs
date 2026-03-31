// ===============================================
// SCRIPT 1: SimpleHeartbeat.cs (VERSION SIMPLE - RECOMMANDÉE)
// ===============================================

using UnityEngine;
using System.Collections;

public class SimpleHeartbeat : MonoBehaviour
{
    [Header("SIMPLE TEST - Mettez à 1 pour entendre")]
    public float masterVolume = 1f;

    [Header("Configuration")]
    public bool autoStart = true;
    public bool useGeneratedSound = true;

    private AudioSource audioSource;
    private bool isPlaying = false;
    private float currentBPM = 60f;

    void Start()
    {
        Debug.Log("?? SIMPLE HEARTBEAT - Démarrage...");
        SetupAudioSource();
        SetupHeartbeatSound();

        if (autoStart)
        {
            Invoke("StartTest", 0.5f);
        }
    }

    void SetupAudioSource()
    {
        AudioSource oldAudio = GetComponent<AudioSource>();
        if (oldAudio != null)
            DestroyImmediate(oldAudio);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = masterVolume;
        audioSource.pitch = 1f;
        audioSource.spatialBlend = 0f;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.priority = 128;

        Debug.Log("? AudioSource créé avec volume: " + audioSource.volume);
    }

    void SetupHeartbeatSound()
    {
        if (useGeneratedSound)
        {
            AudioClip clip = CreateSimpleHeartbeat();
            audioSource.clip = clip;
            Debug.Log("? Son généré: " + clip.name + " - Durée: " + clip.length + "s");
        }
    }

    AudioClip CreateSimpleHeartbeat()
    {
        int sampleRate = 44100;
        float duration = 0.5f;
        int samples = Mathf.RoundToInt(sampleRate * duration);

        AudioClip clip = AudioClip.Create("SimpleHeartbeat", samples, 1, sampleRate, false);
        float[] audioData = new float[samples];

        for (int i = 0; i < samples; i++)
        {
            float time = (float)i / sampleRate;
            float baseSound = Mathf.Sin(2f * Mathf.PI * 80f * time);

            float envelope = 1f;
            if (time < 0.1f)
                envelope = time / 0.1f;
            else if (time > duration - 0.1f)
                envelope = (duration - time) / 0.1f;

            audioData[i] = baseSound * envelope * 0.5f;
        }

        clip.SetData(audioData, 0);
        return clip;
    }

    void StartTest()
    {
        Debug.Log("?? TEST AUDIO IMMÉDIAT...");
        PlayHeartbeatOnce();
        Invoke("StartHeartbeatSequence", 2f);
    }

    public void PlayHeartbeatOnce()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            Debug.Log($"?? LECTURE: Volume={audioSource.volume}, Clip={audioSource.clip.name}");
            audioSource.volume = masterVolume;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("? AudioSource ou clip manquant!");
            LogAudioDebug();
        }
    }

    public void StartHeartbeatSequence()
    {
        if (!isPlaying)
        {
            Debug.Log("?? SÉQUENCE DÉMARRÉE");
            isPlaying = true;
            StartCoroutine(HeartbeatLoop());
        }
    }

    public void StopHeartbeatSequence()
    {
        isPlaying = false;
        StopAllCoroutines();
        Debug.Log("?? SÉQUENCE ARRÊTÉE");
    }

    IEnumerator HeartbeatLoop()
    {
        float elapsedTime = 0f;
        float totalTime = 900f; // 15 minutes

        while (isPlaying && elapsedTime < totalTime)
        {
            CalculateBPM(elapsedTime, totalTime);
            PlayHeartbeatOnce();

            float interval = 60f / currentBPM;
            yield return new WaitForSeconds(interval);

            elapsedTime += interval;

            if (Mathf.FloorToInt(elapsedTime) % 10 == 0)
            {
                Debug.Log($"?? Temps: {elapsedTime:F0}s, BPM: {currentBPM:F1}");
            }
        }

        isPlaying = false;
        Debug.Log("? SÉQUENCE TERMINÉE");
    }

    void CalculateBPM(float elapsed, float total)
    {
        float progress = elapsed / total;

        if (progress < 0.33f) // 0-5 min: Normal
        {
            currentBPM = 60f;
        }
        else if (progress < 0.67f) // 5-10 min: Un peu rapide
        {
            currentBPM = 80f;
        }
        else // 10-15 min: Accélération progressive
        {
            float finalPhase = (progress - 0.67f) / 0.33f;

            // Dernières minutes deviennent de plus en plus rapides
            if (finalPhase > 0.8f) // Dernière minute
            {
                float lastMinute = (finalPhase - 0.8f) / 0.2f;
                currentBPM = Mathf.Lerp(120f, 140f, lastMinute);
            }
            else
            {
                currentBPM = Mathf.Lerp(100f, 120f, finalPhase / 0.8f);
            }
        }
    }

    void LogAudioDebug()
    {
        Debug.Log("=== DEBUG AUDIO ===");
        Debug.Log($"AudioSource existe: {audioSource != null}");
        if (audioSource != null)
        {
            Debug.Log($"Clip assigné: {audioSource.clip != null}");
            Debug.Log($"Volume: {audioSource.volume}");
            Debug.Log($"Enabled: {audioSource.enabled}");
        }

        AudioListener listener = FindObjectOfType<AudioListener>();
        Debug.Log($"AudioListener trouvé: {listener != null}");
        Debug.Log($"Volume principal Unity: {AudioListener.volume}");
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 200));
        GUILayout.Box("?? SIMPLE HEARTBEAT TEST");

        GUILayout.Label($"Volume: {masterVolume:F2}");
        masterVolume = GUILayout.HorizontalSlider(masterVolume, 0f, 1f);

        if (audioSource != null)
            audioSource.volume = masterVolume;

        if (GUILayout.Button("?? TEST 1 BATTEMENT"))
        {
            PlayHeartbeatOnce();
        }

        if (GUILayout.Button("?? DÉMARRER SÉQUENCE"))
        {
            StartHeartbeatSequence();
        }

        if (GUILayout.Button("?? ARRÊTER"))
        {
            StopHeartbeatSequence();
        }

        if (GUILayout.Button("?? DEBUG INFO"))
        {
            LogAudioDebug();
        }

        GUILayout.Label($"Status: {(isPlaying ? "?? ACTIF" : "?? ARRÊTÉ")}");
        GUILayout.Label($"BPM Actuel: {currentBPM:F1}");

        GUILayout.EndArea();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayHeartbeatOnce();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isPlaying)
                StopHeartbeatSequence();
            else
                StartHeartbeatSequence();
        }
    }
}
