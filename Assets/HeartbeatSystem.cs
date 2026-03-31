
// ===============================================
// SCRIPT 2: HeartbeatSystem.cs (VERSION AVANCÉE)
// ===============================================

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HeartbeatSystem : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource heartbeatAudioSource;
    public AudioClip heartbeatSound;

    [Header("Timing Settings")]
    public float totalDuration = 900f; // 15 minutes

    [Header("Heartbeat Phases")]
    public float phase1BPM = 60f;
    public float phase2BPM = 80f;
    public float phase3StartBPM = 100f;
    public float phase3EndBPM = 140f;

    [Header("Audio Effects")]
    [Range(0.1f, 2f)]
    public float volumeIntensity = 1f;
    [Range(0.8f, 1.2f)]
    public float pitchVariation = 0.1f;
    public bool useVolumeIncrease = true;
    public bool usePitchIncrease = true;

    [Header("Visual Effects")]
    public Light heartbeatLight;
    public Color normalColor = Color.red;
    public Color intenseColor = Color.white;

    [Header("Debug")]
    public bool showDebugInfo = true;

    private float currentBPM;
    private float currentHeartbeatInterval;
    private float elapsedTime = 0f;
    private int currentPhase = 1;
    private float baseVolume;
    private float basePitch;
    private bool isActive = false;

    public AnimationCurve volumeCurve = AnimationCurve.EaseInOut(0, 1, 1, 1.5f);
    public AnimationCurve pitchCurve = AnimationCurve.EaseInOut(0, 1, 1, 1.2f);

    void Start()
    {
        SetupAudioSource();

        if (heartbeatLight == null)
            heartbeatLight = GetComponent<Light>();
    }

    void SetupAudioSource()
    {
        if (heartbeatAudioSource == null)
        {
            heartbeatAudioSource = gameObject.AddComponent<AudioSource>();
        }

        heartbeatAudioSource.clip = heartbeatSound;
        heartbeatAudioSource.playOnAwake = false;
        heartbeatAudioSource.loop = false;
        heartbeatAudioSource.volume = 0.8f;
        heartbeatAudioSource.pitch = 1f;
        heartbeatAudioSource.spatialBlend = 0f;
        heartbeatAudioSource.priority = 128;
        heartbeatAudioSource.enabled = true;

        if (heartbeatSound == null)
        {
            Debug.LogError("ERREUR: Aucun AudioClip assigné!");

            HeartbeatSoundGenerator generator = GetComponent<HeartbeatSoundGenerator>();
            if (generator == null)
                generator = gameObject.AddComponent<HeartbeatSoundGenerator>();

            heartbeatSound = generator.GenerateHeartbeatClip();
            heartbeatAudioSource.clip = heartbeatSound;

            Debug.Log("Son de battement généré automatiquement");
        }

        baseVolume = heartbeatAudioSource.volume;
        basePitch = heartbeatAudioSource.pitch;

        Debug.Log($"AudioSource configuré - Volume: {baseVolume}, Clip: {heartbeatSound?.name}");
    }

    public void StartHeartbeatSequence()
    {
        if (!isActive)
        {
            TestAudio();
            isActive = true;
            elapsedTime = 0f;
            currentPhase = 1;
            StartCoroutine(HeartbeatSequence());

            if (showDebugInfo)
                Debug.Log("Séquence démarrée - 15 minutes");
        }
    }

    void TestAudio()
    {
        if (heartbeatAudioSource != null && heartbeatSound != null)
        {
            Debug.Log("TEST AUDIO: Lecture test...");
            heartbeatAudioSource.volume = 1f;
            heartbeatAudioSource.PlayOneShot(heartbeatSound);
        }
        else
        {
            Debug.LogError("ERREUR TEST AUDIO!");
        }
    }

    public void StopHeartbeatSequence()
    {
        isActive = false;
        StopAllCoroutines();

        if (showDebugInfo)
            Debug.Log("Séquence arrêtée");
    }

    IEnumerator HeartbeatSequence()
    {
        while (isActive && elapsedTime < totalDuration)
        {
            UpdateCurrentPhase();
            CalculateCurrentBPM();
            PlayHeartbeat();

            yield return new WaitForSeconds(currentHeartbeatInterval);
            elapsedTime += currentHeartbeatInterval;
        }

        isActive = false;
        if (showDebugInfo)
            Debug.Log("Séquence terminée");
    }

    void UpdateCurrentPhase()
    {
        float phaseTime = totalDuration / 3f;

        if (elapsedTime < phaseTime)
        {
            currentPhase = 1;
        }
        else if (elapsedTime < phaseTime * 2)
        {
            currentPhase = 2;
        }
        else
        {
            currentPhase = 3;
        }
    }

    void CalculateCurrentBPM()
    {
        float phaseTime = totalDuration / 3f;

        switch (currentPhase)
        {
            case 1:
                currentBPM = phase1BPM;
                break;

            case 2:
                currentBPM = phase2BPM;
                break;

            case 3:
                float phase3ElapsedTime = elapsedTime - (phaseTime * 2);
                float phase3Progress = phase3ElapsedTime / phaseTime;

                if (phase3Progress > 0.8f) // Dernière minute
                {
                    float finalMinuteProgress = (phase3Progress - 0.8f) / 0.2f;
                    currentBPM = Mathf.Lerp(phase3StartBPM * 1.2f, phase3EndBPM, finalMinuteProgress);
                }
                else
                {
                    currentBPM = Mathf.Lerp(phase3StartBPM, phase3StartBPM * 1.2f, phase3Progress / 0.8f);
                }
                break;
        }

        currentHeartbeatInterval = 60f / currentBPM;

        if (showDebugInfo && Time.frameCount % 60 == 0)
        {
            Debug.Log($"Phase: {currentPhase}, BPM: {currentBPM:F1}, Temps: {elapsedTime:F1}s");
        }
    }

    void PlayHeartbeat()
    {
        if (heartbeatSound == null)
        {
            Debug.LogError("Aucun son!");
            return;
        }

        if (heartbeatAudioSource == null)
        {
            Debug.LogError("AudioSource manquant!");
            return;
        }

        float totalProgress = elapsedTime / totalDuration;

        float volumeMultiplier = useVolumeIncrease ?
            volumeCurve.Evaluate(totalProgress) * volumeIntensity : 1f;
        heartbeatAudioSource.volume = Mathf.Clamp01(baseVolume * volumeMultiplier * 1.5f);

        if (usePitchIncrease)
        {
            float pitchMultiplier = pitchCurve.Evaluate(totalProgress);
            heartbeatAudioSource.pitch = basePitch * pitchMultiplier;
        }

        if (heartbeatAudioSource.isActiveAndEnabled)
        {
            heartbeatAudioSource.PlayOneShot(heartbeatSound, heartbeatAudioSource.volume);

            if (showDebugInfo && Time.frameCount % 30 == 0)
            {
                Debug.Log($"AUDIO: Volume={heartbeatAudioSource.volume:F2}, BPM={currentBPM:F1}");
            }
        }
        else
        {
            Debug.LogError("AudioSource inactif!");
        }

        if (heartbeatLight != null)
        {
            StartCoroutine(HeartbeatLightEffect(totalProgress));
        }
    }

    IEnumerator HeartbeatLightEffect(float intensity)
    {
        if (heartbeatLight != null)
        {
            Color originalColor = heartbeatLight.color;
            float originalIntensity = heartbeatLight.intensity;

            Color targetColor = Color.Lerp(normalColor, intenseColor, intensity);
            float targetIntensity = originalIntensity * (1f + intensity);

            heartbeatLight.color = targetColor;
            heartbeatLight.intensity = targetIntensity;

            yield return new WaitForSeconds(0.1f);

            heartbeatLight.color = originalColor;
            heartbeatLight.intensity = originalIntensity;
        }
    }

    // Méthodes publiques
    public void SetCustomDuration(float minutes) { totalDuration = minutes * 60f; }
    public void SetPhase1BPM(float bpm) { phase1BPM = bpm; }
    public void SetPhase2BPM(float bpm) { phase2BPM = bpm; }
    public void SetPhase3BPM(float startBpm, float endBpm)
    {
        phase3StartBPM = startBpm;
        phase3EndBPM = endBpm;
    }

    // Propriétés
    public float CurrentBPM => currentBPM;
    public int CurrentPhase => currentPhase;
    public float ElapsedTime => elapsedTime;
    public float RemainingTime => totalDuration - elapsedTime;
    public bool IsActive => isActive;
    public float ProgressPercent => (elapsedTime / totalDuration) * 100f;

    void OnGUI()
    {
        if (showDebugInfo && isActive)
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 150));
            GUILayout.Box("Heartbeat System Debug");
            GUILayout.Label($"Phase: {currentPhase}/3");
            GUILayout.Label($"BPM: {currentBPM:F1}");
            GUILayout.Label($"Temps: {elapsedTime:F1}s / {totalDuration:F0}s");
            GUILayout.Label($"Progression: {ProgressPercent:F1}%");
            GUILayout.Label($"Restant: {RemainingTime:F1}s");
            GUILayout.EndArea();
        }
    }
}
