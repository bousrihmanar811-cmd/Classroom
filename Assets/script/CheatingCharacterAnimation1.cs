//using UnityEngine;

//public class CheatingCharacterAnimation1 : MonoBehaviour
//{
//    public Animator animator;                 // Animator du personnage
//    public float appearAfter = 200f;          // Apparition après X secondes
//    public float disappearAfter = 350f;       // Disparition après Y secondes
//    public AudioSource audioSource;           // AudioSource à assigner dans l'Inspector
//    public AudioClip[] audioClips;            // 4 clips à jouer (à assigner dans l'Inspector)
//    public float delayBetweenClips = 7f;      // Délai entre les sons (secondes)

//    private int currentClipIndex = 0;

//    void Start()
//    {
//        gameObject.SetActive(false);                   // Masquer le personnage au début
//        Invoke("AppearAndAnimate", appearAfter);       // Planifie l’apparition
//    }

//    void AppearAndAnimate()
//    {
//        gameObject.SetActive(true);                    // Le personnage apparaît
//        animator.enabled = true;                       // Lance l’animation
//        PlayNextAudio();                               // Démarre la lecture audio
//        Invoke("Disappear", disappearAfter);           // Planifie la disparition
//    }

//    void PlayNextAudio()
//    {
//        if (audioClips.Length == 0 || audioSource == null) return;

//        if (currentClipIndex < audioClips.Length)
//        {
//            audioSource.clip = audioClips[currentClipIndex];
//            audioSource.Play();
//            //float clipLength = audioClips[currentClipIndex].length;
//            //currentClipIndex++;
//            //Invoke("PlayNextAudio", clipLength);
//            float waitTime = audioClips[currentClipIndex].length + delayBetweenClips;
//            currentClipIndex++;

//            Invoke("PlayNextAudio", waitTime);
//        }
//    }

//    void Disappear()
//    {
//        CancelInvoke("PlayNextAudio");                 // Stoppe toute lecture prévue
//        if (audioSource != null && audioSource.isPlaying)
//        {
//            audioSource.Stop();                        // Arrête le son s’il joue encore
//        }
//        gameObject.SetActive(false);                   // Disparition du personnage
//    }
//}

using UnityEngine;

public class CheatingCharacterAnimation1 : MonoBehaviour
{
    public Animator animator;
    public float appearAfter = 200f;
    public float disappearAfter = 350f;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public float delayBetweenClips = 7f;

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

        // durée relative entre disparition et apparition
        Invoke("Disappear", disappearAfter - appearAfter);
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
        {
            audioSource.Stop();
        }
        gameObject.SetActive(false);
    }
}
