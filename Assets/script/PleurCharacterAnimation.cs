//using UnityEngine;

//public class PleurCharacterAnimation : MonoBehaviour
//{
//    public Animator animator;               // Animator du personnage
//    public float appearAfter = 720f;        // Par exemple, il apparaît après 2 minutes (à ajuster)
//    public float disappearAfter = 770f;     // 3 minutes après l'apparition
//    public AudioSource audioSource;         // AudioSource assigné dans l'inspecteur
//    void Start()
//    {
//        gameObject.SetActive(false);                   // Masquer le personnage au début
//        Invoke("AppearAndAnimate", appearAfter);       // Planifie l’apparition
//    }

//    void AppearAndAnimate()
//    {
//        gameObject.SetActive(true);                    // Le personnage apparaît
//        animator.enabled = true;                       // Lance l’animation
//        if (audioSource != null)
//        {
//            audioSource.Play();             // Joue le son
//        }
//        Invoke("Disappear", disappearAfter);           // Planifie la disparition 3 min plus tard
//    }

//    void Disappear()
//    {
//        if (audioSource != null && audioSource.isPlaying)
//        {
//            audioSource.Stop();             // Arrête le son
//        }
//        gameObject.SetActive(false);                   // Disparition du personnage
//    }
//}

using UnityEngine;

public class PleurCharacterAnimation : MonoBehaviour
{
    public Animator animator;
    public float appearAfter = 520f;
    public float disappearAfter = 570f;
    public AudioSource audioSource;

    void Start()
    {
        gameObject.SetActive(false);
        Invoke("AppearAndAnimate", appearAfter);
    }

    void AppearAndAnimate()
    {
        gameObject.SetActive(true);
        animator.enabled = true;
        if (audioSource != null)
        {
            audioSource.Play();
        }
        // Correction : durée visible = disappearAfter - appearAfter
        Invoke("Disappear", disappearAfter - appearAfter);
    }

    void Disappear()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        gameObject.SetActive(false);
    }
}
