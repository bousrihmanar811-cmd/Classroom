//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FootCharacterAnimation : MonoBehaviour
//{
//    public Animator animator;               // Animator du personnage
//    public float appearAfter = 120f;        // Par exemple, il apparaît après 2 minutes (à ajuster)
//    public float disappearAfter = 150f;     // 3 minutes après l'apparition
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCharacterAnimation : MonoBehaviour
{
    public Animator animator;               // Animator du personnage
    public AudioSource audioSource;         // AudioSource assigné dans l’inspecteur

    [Header("Cycle 1")]
    public float appearAfter1 = 120f;       // 1ère apparition
    public float disappearAfter1 = 150f;    // 1ère disparition

    [Header("Cycle 2")]
    public float appearAfter2 = 500f;       // 2ème apparition
    public float disappearAfter2 = 530f;    // 2ème disparition

    void Start()
    {
        gameObject.SetActive(false); // Masquer le personnage au début

        // Planifier le cycle 1
        Invoke(nameof(AppearAndAnimate1), appearAfter1);
        Invoke(nameof(Disappear), disappearAfter1);

        // Planifier le cycle 2
        Invoke(nameof(AppearAndAnimate2), appearAfter2);
        Invoke(nameof(Disappear), disappearAfter2);
    }

    void AppearAndAnimate1()
    {
        AppearAndAnimate();
    }

    void AppearAndAnimate2()
    {
        AppearAndAnimate();
    }

    void AppearAndAnimate()
    {
        gameObject.SetActive(true);   // Le personnage apparaît
        animator.enabled = true;      // Lance l’animation
        if (audioSource != null)
        {
            audioSource.Play();       // Joue le son
        }
    }

    void Disappear()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();       // Arrête le son
        }
        gameObject.SetActive(false);  // Disparition du personnage
    }
}
