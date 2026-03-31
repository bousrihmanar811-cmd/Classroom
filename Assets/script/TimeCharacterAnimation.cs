using UnityEngine;

public class TimeCharacterAnimation : MonoBehaviour
{
    public Animator animator;                  // Animator du personnage
    public float appearAfter = 290f;           // Temps avant la première apparition (en secondes)
    public float repeatInterval = 300f;        // Intervalle entre chaque répétition (5 min)
    public float visibleDuration = 10f;        // Temps d'apparition (en secondes)
    public float finalDisappearTime = 1020f;   // Temps total avant disparition finale (17 min)

    public AudioSource audioSource;            // Source audio
    public AudioClip[] audioClips;             // Liste des sons (3 sons différents)

    private int repetitionCount = 0;
    private int maxRepetitions = 3;
    private int currentClipIndex = 0;

    void Start()
    {
        gameObject.SetActive(false); // Masquer le personnage au départ

        Invoke("AppearAndAnimate", appearAfter); // Début des apparitions

        // Disparition totale après 17 min
        Invoke("FinalDisappear", finalDisappearTime);
    }

    void AppearAndAnimate()
    {
        if (repetitionCount >= maxRepetitions)
            return; // Stop après 3 répétitions

        gameObject.SetActive(true);
        animator.enabled = true;

        // Jouer le son associé
        if (audioSource != null && audioClips.Length > 0)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
            currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
        }

        Invoke("Disappear", visibleDuration); // Disparaît après x secondes

        repetitionCount++;
        if (repetitionCount < maxRepetitions)
        {
            Invoke("AppearAndAnimate", repeatInterval); // Répéter après 5 min
        }
    }

    void Disappear()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        gameObject.SetActive(false);
    }

    void FinalDisappear()
    {
        // Stoppe tout : audio, animation, et cache définitivement
        CancelInvoke("AppearAndAnimate");
        CancelInvoke("Disappear");

        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();

        gameObject.SetActive(false);
    }
}
