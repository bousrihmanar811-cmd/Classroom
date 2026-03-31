using UnityEngine;

public class BoxCharacterAnimation : MonoBehaviour
{
    public Animator animator;               // Animator du personnage
    public float appearAfter = 300f;        // Apparition à 300s
    public float disappearAfter = 310f;     // Disparition à 310s
    public AudioSource audioSource;         // AudioSource assigné dans l’inspecteur

    void Start()
    {
        gameObject.SetActive(false);
        Invoke("AppearAndAnimate", appearAfter);       // Planifie l’apparition
    }

    void AppearAndAnimate()
    {
        gameObject.SetActive(true);
        animator.enabled = true;

        if (audioSource != null)
            audioSource.Play();

        // Ici on calcule le temps relatif (310 - 300 = 10s)
        Invoke("Disappear", disappearAfter - appearAfter);
    }

    void Disappear()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();

        gameObject.SetActive(false);
    }
}
