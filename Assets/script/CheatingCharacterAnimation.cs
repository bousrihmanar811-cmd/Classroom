using UnityEngine;

public class CheatingCharacterAnimation : MonoBehaviour
{
    public Animator animator;
    public float appearAfter = 180f;          // Apparition à 180s
    public float disappearAfter = 300f;       // Disparition à 300s (absolu)
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public float delayBetweenClips = 27f;

    private int currentClipIndex = 0;

    void Start()
    {
        gameObject.SetActive(false);
        Invoke("AppearAndAnimate", appearAfter);
    }

    void AppearAndAnimate()
    {
        gameObject.SetActive(true);
        animator.enabled = true;
        PlayNextAudio();

        // ?? différence entre disparition absolue et apparition
        float timeVisible = disappearAfter - appearAfter;
        if (timeVisible > 0)
            Invoke("Disappear", timeVisible);
    }

    void PlayNextAudio()
    {
        if (audioClips.Length == 0 || audioSource == null) return;

        if (currentClipIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
            float waitTime = audioClips[currentClipIndex].length + delayBetweenClips;
            currentClipIndex++;
            Invoke("PlayNextAudio", waitTime);
        }
    }

    void Disappear()
    {
        CancelInvoke("PlayNextAudio");
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();

        gameObject.SetActive(false);
    }
}
