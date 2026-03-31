using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Récupérer le composant AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // Méthode pour jouer l'audio
    public void PlayAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    //// Méthode pour arrêter l'audio
    //public void StopAudio()
    //{
    //    if (audioSource.isPlaying)
    //    {
    //        audioSource.Stop();
    //    }
    //}
}