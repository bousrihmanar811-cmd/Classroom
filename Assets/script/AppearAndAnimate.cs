//using UnityEngine;

//public class AppearAndAnimate : MonoBehaviour
//{
//    public Animator animator;               // Animator du personnage
//    public AudioSource audioSource;         // AudioSource assigné dans l'inspecteur

//    public float appearAfter = 965f;        // Apparition après 965s
//    public float disappearAfter = 990f;     // Disparition après 990s

//    void Start()
//    {
//        gameObject.SetActive(false);        // Masquer au démarrage
//        Invoke("MakeAppear", appearAfter);  // Planifier l'apparition
//        Invoke("Disappear", disappearAfter); // Planifier la disparition
//    }

//    void MakeAppear()
//    {
//        gameObject.SetActive(true);         // Affiche le personnage
//        animator.enabled = true;            // Lance l'animation

//        if (audioSource != null)
//        {
//            audioSource.Play();             // Joue le son
//        }
//    }

//    void Disappear()
//    {
//        if (audioSource != null && audioSource.isPlaying)
//        {
//            audioSource.Stop();             // Arrête le son
//        }

//        gameObject.SetActive(false);        // Cache le personnage
//    }
//}
using UnityEngine;

public class AppearAndAnimate : MonoBehaviour
{
    public Animator animator;               // Animator du personnage
    public AudioSource audioSource;         // Son joué à l'apparition
    public AudioSource songAt990;           // Son qui commence à 990s

    public float appearAfter = 965f;        // Apparition après 965s
    public float disappearAfter = 990f;     // Disparition après 990s

    void Start()
    {
        gameObject.SetActive(false);
        Invoke("MakeAppear", appearAfter);
        Invoke("Disappear", disappearAfter);
        Invoke("PlaySongAt990", disappearAfter); // Joue la musique à 990s
    }

    void MakeAppear()
    {
        gameObject.SetActive(true);
        animator.enabled = true;

        if (audioSource != null)
        {
            audioSource.Play();
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

    void PlaySongAt990()
    {
        if (songAt990 != null)
        {
            songAt990.Play();  // Lance la nouvelle musique
        }
    }
}
