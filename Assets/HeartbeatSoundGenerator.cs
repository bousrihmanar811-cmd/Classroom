
// ===============================================
// SCRIPT 3: HeartbeatSoundGenerator.cs
// ===============================================

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HeartbeatSoundGenerator : MonoBehaviour
{
    [Header("Sound Generation")]
    public int sampleRate = 44100;
    public float heartbeatDuration = 0.8f;

    [Header("Sound Characteristics")]
    [Range(20f, 200f)]
    public float baseFrequency = 60f;
    [Range(0.1f, 1f)]
    public float volume = 0.7f;

    [Header("Double Beat")]
    public bool useDoubleBeat = true;
    public float firstBeatDuration = 0.15f;
    public float pauseDuration = 0.1f;
    public float secondBeatDuration = 0.12f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GenerateHeartbeatClip();
    }

    public AudioClip GenerateHeartbeatClip()
    {
        int totalSamples = Mathf.RoundToInt(sampleRate * heartbeatDuration);
        AudioClip heartbeatClip = AudioClip.Create("GeneratedHeartbeat", totalSamples, 1, sampleRate, false);

        float[] samples = new float[totalSamples];

        if (useDoubleBeat)
        {
            GenerateDoubleBeat(samples, totalSamples);
        }
        else
        {
            GenerateSingleBeat(samples, totalSamples);
        }

        heartbeatClip.SetData(samples, 0);
        audioSource.clip = heartbeatClip;

        return heartbeatClip;
    }

    void GenerateDoubleBeat(float[] samples, int totalSamples)
    {
        int firstBeatSamples = Mathf.RoundToInt(sampleRate * firstBeatDuration);
        int pauseSamples = Mathf.RoundToInt(sampleRate * pauseDuration);
        int secondBeatSamples = Mathf.RoundToInt(sampleRate * secondBeatDuration);

        // Premier battement "Lub"
        GenerateBeatWaveform(samples, 0, firstBeatSamples, baseFrequency, 1f);

        // Pause
        for (int i = firstBeatSamples; i < firstBeatSamples + pauseSamples && i < totalSamples; i++)
        {
            samples[i] = 0f;
        }

        // Deuxième battement "Dub"
        int secondBeatStart = firstBeatSamples + pauseSamples;
        GenerateBeatWaveform(samples, secondBeatStart, secondBeatSamples, baseFrequency * 1.3f, 0.8f);
    }

    void GenerateSingleBeat(float[] samples, int totalSamples)
    {
        GenerateBeatWaveform(samples, 0, totalSamples, baseFrequency, 1f);
    }

    void GenerateBeatWaveform(float[] samples, int startIndex, int duration, float frequency, float amplitude)
    {
        for (int i = 0; i < duration && (startIndex + i) < samples.Length; i++)
        {
            float time = i / (float)sampleRate;
            float envelope = CalculateEnvelope(time, duration / (float)sampleRate);

            float wave = 0f;
            wave += Mathf.Sin(2f * Mathf.PI * frequency * time) * 0.6f;
            wave += Mathf.Sin(2f * Mathf.PI * frequency * 2f * time) * 0.2f;
            wave += Mathf.Sin(2f * Mathf.PI * frequency * 3f * time) * 0.1f;
            wave += (Random.Range(-1f, 1f) * 0.05f);

            samples[startIndex + i] = wave * envelope * amplitude * volume;
        }
    }

    float CalculateEnvelope(float time, float totalDuration)
    {
        float attackTime = totalDuration * 0.1f;
        float decayTime = totalDuration * 0.4f;
        float sustainLevel = 0.3f;
        float releaseTime = totalDuration * 0.5f;

        if (time < attackTime)
        {
            return time / attackTime;
        }
        else if (time < attackTime + decayTime)
        {
            float decayProgress = (time - attackTime) / decayTime;
            return Mathf.Lerp(1f, sustainLevel, decayProgress);
        }
        else if (time < totalDuration - releaseTime)
        {
            return sustainLevel;
        }
        else
        {
            float releaseProgress = (time - (totalDuration - releaseTime)) / releaseTime;
            return Mathf.Lerp(sustainLevel, 0f, releaseProgress);
        }
    }

    public void SetFrequency(float newFrequency)
    {
        baseFrequency = newFrequency;
        GenerateHeartbeatClip();
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        GenerateHeartbeatClip();
    }

    public void SetDuration(float newDuration)
    {
        heartbeatDuration = newDuration;
        GenerateHeartbeatClip();
    }

    public void SetNormalHeartbeat()
    {
        baseFrequency = 60f;
        volume = 0.7f;
        heartbeatDuration = 0.8f;
        GenerateHeartbeatClip();
    }

    public void SetStressedHeartbeat()
    {
        baseFrequency = 80f;
        volume = 0.9f;
        heartbeatDuration = 0.6f;
        GenerateHeartbeatClip();
    }

    public void SetIntenseHeartbeat()
    {
        baseFrequency = 100f;
        volume = 1f;
        heartbeatDuration = 0.4f;
        GenerateHeartbeatClip();
    }
}