using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DynamicHeartbeat: MonoBehaviour
{
    public float bpm = 70f;       // beats per minute (start normal ~70 bpm)
    public float time;            // timer
    public float sampleRate;      // audio sample rate

    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        var audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.spatialBlend = 0f; // 2D sound
        audioSource.Play(); // force play
    }

    void Update()
    {
        // Heartbeat speed progression
        float elapsed = Time.time / 60f; // minutes passed

        if (elapsed < 5) bpm = 70f;                 // 0-5 min normal
        else if (elapsed < 10) bpm = 90f;           // 5-10 min a bit faster
        else if (elapsed < 15) bpm = 110f;          // 10-15 min faster
        else bpm = Mathf.Lerp(110f, 150f, (elapsed - 15f) / 5f); // 15-20 min very fast
    }

    // Generate synthetic sound
    void OnAudioFilterRead(float[] data, int channels)
    {
        float freq = bpm / 60f; // beats per second
        for (int i = 0; i < data.Length; i += channels)
        {
            time += freq / sampleRate;
            if (time > 1f) time -= 1f;

            // Simple "thump" sound
            float sample = Mathf.Exp(-time * 15f) * Mathf.Sin(2f * Mathf.PI * 50f * time);

            for (int c = 0; c < channels; c++)
                data[i + c] = sample * 0.3f; // volume
        }
    }
}
