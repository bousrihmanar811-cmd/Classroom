using UnityEngine;

public class HeartbeatAudioDebug : MonoBehaviour
{
    [Header("Test Components")]
    public HeartbeatSystem heartbeatSystem;
    public HeartbeatSoundGenerator soundGenerator;

    [Header("Manual Testing")]
    public KeyCode testKey = KeyCode.Space;
    public KeyCode startSequenceKey = KeyCode.Return;

    [Header("Audio Diagnostics")]
    public bool showDetailedInfo = true;

    void Start()
    {
        if (heartbeatSystem == null)
            heartbeatSystem = FindObjectOfType<HeartbeatSystem>();

        if (soundGenerator == null)
            soundGenerator = FindObjectOfType<HeartbeatSoundGenerator>();

        PerformAudioDiagnostic();
        Invoke("AutoTest", 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(testKey))
        {
            TestSingleHeartbeat();
        }

        if (Input.GetKeyDown(startSequenceKey))
        {
            StartHeartbeatSequence();
        }
    }

    void AutoTest()
    {
        Debug.Log("=== TEST AUTOMATIQUE AUDIO ===");
        TestSingleHeartbeat();
    }

    public void TestSingleHeartbeat()
    {
        Debug.Log("?? TEST: Battement unique...");

        if (heartbeatSystem == null)
        {
            Debug.LogError("? HeartbeatSystem introuvable!");
            return;
        }

        AudioSource audio = heartbeatSystem.heartbeatAudioSource;
        if (audio == null)
        {
            Debug.LogError("? AudioSource manquant!");
            return;
        }

        if (audio.clip == null)
        {
            Debug.LogWarning("?? Aucun AudioClip - Génération...");

            if (soundGenerator == null)
                soundGenerator = gameObject.AddComponent<HeartbeatSoundGenerator>();

            audio.clip = soundGenerator.GenerateHeartbeatClip();
        }

        if (audio.clip != null)
        {
            audio.volume = 1f;
            audio.pitch = 1f;
            audio.PlayOneShot(audio.clip);
            Debug.Log($"? Son joué: {audio.clip.name}, Volume: {audio.volume}");
        }
        else
        {
            Debug.LogError("? Impossible de jouer!");
        }
    }

    public void StartHeartbeatSequence()
    {
        if (heartbeatSystem != null)
        {
            Debug.Log("?? Séquence complète...");
            heartbeatSystem.StartHeartbeatSequence();
        }
    }

    void PerformAudioDiagnostic()
    {
        Debug.Log("=== DIAGNOSTIC AUDIO ===");

        AudioConfiguration config = AudioSettings.GetConfiguration();
        Debug.Log($"?? Sample Rate: {config.sampleRate}Hz");
        Debug.Log($"?? Speaker Mode: {config.speakerMode}");

        AudioListener listener = FindObjectOfType<AudioListener>();
        if (listener == null)
        {
            Debug.LogWarning("?? Aucun AudioListener!");
        }
        else
        {
            Debug.Log($"?? AudioListener: {listener.gameObject.name}");
        }

        Debug.Log($"Volume principal: {AudioListener.volume}");
    }

    void OnGUI()
    {
        if (!showDetailedInfo) return;

        GUILayout.BeginArea(new Rect(10, 220, 400, 300));
        GUILayout.Box("?? HEARTBEAT DEBUG");

        GUILayout.Label($"[{testKey}] = Test unique");
        GUILayout.Label($"[{startSequenceKey}] = Séquence complète");

        if (GUILayout.Button("?? TEST AUDIO"))
        {
            TestSingleHeartbeat();
        }

        if (GUILayout.Button("?? DÉMARRER"))
        {
            StartHeartbeatSequence();
        }

        if (GUILayout.Button("?? ARRÊTER"))
        {
            if (heartbeatSystem != null)
                heartbeatSystem.StopHeartbeatSequence();
        }

        if (GUILayout.Button("?? DIAGNOSTIC"))
        {
            PerformAudioDiagnostic();
        }

        GUILayout.Space(10);

        if (heartbeatSystem != null)
        {
            GUILayout.Label($"Status: {(heartbeatSystem.IsActive ? "?? ACTIF" : "?? INACTIF")}");
            if (heartbeatSystem.IsActive)
            {
                GUILayout.Label($"BPM: {heartbeatSystem.CurrentBPM:F1}");
                GUILayout.Label($"Phase: {heartbeatSystem.CurrentPhase}/3");
                GUILayout.Label($"Temps: {heartbeatSystem.ElapsedTime:F1}s");
            }
        }

        GUILayout.EndArea();
    }
}