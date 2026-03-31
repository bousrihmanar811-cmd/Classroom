//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CharacterAppearAndAnimate : MonoBehaviour
//{
//    // Start is called before the first frame update
//    public Animator animator;
//    public float delay = 40f;
//    public float appeareAfter=900f;
//    void Start()
//    {
//        gameObject.SetActive(false);// Cache le personnage au début
//        //animator.enabled = false;
//        //invoke("StartAnimation", delay);
//        invoke("AppeareCharacter",appeareAfter);
//    }
//    // Update is called once per frame
//    void AppeareCharacter()
//    {
//        gameObject.SetActive(true);         // Rend visible le personnage
//        animator.enabled = false;           // Ne pas jouer l'animation tout de suite

//        // Planifie l'animation plus tard
//        Invoke("StartAnimation", animationDelay - appearAfter); // Animation 15 min après le début

//    }
//    void StartAnimation()
//    {
//        animator.enabled=true;
//    }


//}

//using UnityEngine;

//public class CharacterAppearAndAnimate : MonoBehaviour
//{
//    public Animator animator;                // L'Animator de ce personnage
//    public AudioSource audioSource;          // Source audio à assigner dans l’inspecteur
//    public float appearAfter = 30f;          // Apparition après 40 secondes
//    public float animationDelay = 910f;      // Animation après 15 minutes
//    public float disappearAfter = 960f;      // Disparition après 16 minutes

//    void Start()
//    {
//        gameObject.SetActive(false);         // Masquer au départ
//        Invoke("MakeAppear", appearAfter);   // Planifier apparition
//        Invoke("Disappear", disappearAfter); // Planifier disparition
//    }

//    void MakeAppear()
//    {
//        gameObject.SetActive(true);          // Affiche le personnage
//        animator.enabled = false;            // Désactive l’animation au départ

//        // Planifie le démarrage de l’animation
//        Invoke("StartAnimation", animationDelay - appearAfter);
//    }

//    void StartAnimation()
//    {
//        animator.enabled = true;             // Active l’animation
//        if (audioSource != null)
//        {
//            audioSource.Play();              // Joue le son
//        }
//    }

//    void Disappear()
//    {
//        if (audioSource != null && audioSource.isPlaying)
//        {
//            audioSource.Stop();              // Arrête le son
//        }

//        gameObject.SetActive(false);         // Masque le personnage
//    }
//}





using UnityEngine;

public class CharacterAppearAndAnimate : MonoBehaviour
{
    public Animator animator;                // L'Animator de ce personnage
    public AudioSource audioSource;          // Source audio à assigner dans l’inspecteur

    [Header("Timings (secondes)")]
    public float appearAfter = 30f;          // Apparition initiale
    public float disappearAt90 = 90f;        // Disparition à 90s
    public float appearAt350 = 350f;         // Réapparition à 350s
    public float disappearAt450 = 450f;      // Disparition à 450s
    public float appearAt500 = 500f;         // Réapparition à 500s
    public float animationDelay = 910f;      // Animation à 15 minutes
    public float disappearAfter = 960f;      // Disparition finale (16 minutes)

    void Start()
    {
        gameObject.SetActive(false);         // Masqué au départ

        // Planification des apparitions/disparitions
        Invoke(nameof(MakeAppear), appearAfter);
        Invoke(nameof(Disappear), disappearAt90);
        Invoke(nameof(MakeAppear), appearAt350);
        Invoke(nameof(Disappear), disappearAt450);
        Invoke(nameof(MakeAppear), appearAt500);
        Invoke(nameof(Disappear), disappearAfter);
    }

    void MakeAppear()
    {
        gameObject.SetActive(true);          // Affiche le personnage
        animator.enabled = false;            // Désactive l’animation au départ

        // Planifie le démarrage de l’animation (si pas déjà lancé)
        float delay = animationDelay - Time.time;
        if (delay > 0)
        {
            CancelInvoke(nameof(StartAnimation));
            Invoke(nameof(StartAnimation), delay);
        }
    }

    void StartAnimation()
    {
        animator.enabled = true;             // Active l’animation
        if (audioSource != null)
        {
            audioSource.Play();              // Joue le son
        }
    }

    void Disappear()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();              // Arrête le son
        }
        gameObject.SetActive(false);         // Masque le personnage
    }
}
